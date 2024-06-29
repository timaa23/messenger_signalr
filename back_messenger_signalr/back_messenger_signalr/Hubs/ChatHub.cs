using back_messenger_signalr.Helpers;
using back_messenger_signalr.Models;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace back_messenger_signalr.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageService _messageService;
        private readonly IConversationRepository _conversationRepository;
        private readonly IDictionary<string, UserRoomConnection> _connections;
        private readonly OnlineDB _onlineDB;
        public ChatHub(IDictionary<string, UserRoomConnection> connections, OnlineDB onlineDB, IMessageService messageService, IConversationRepository conversationRepository)
        {
            _connections = connections;
            _onlineDB = onlineDB;
            _messageService = messageService;
            _conversationRepository = conversationRepository;
        }

        public override Task OnConnectedAsync()
        {
            var userID = Context.UserIdentifier;

            Console.WriteLine($"USER WITH ID: #{userID} HAS BEEN CONNECTED");

            return base.OnConnectedAsync();
        }

        public async Task<bool> TestAsync(string message, Guid guid)
        {
            var user = Context.UserIdentifier;

            var participants = await _conversationRepository.GetAll()
                .Where(c => c.Guid.Equals(guid))
                .Select(c => c.Participants)
                .FirstOrDefaultAsync();

            foreach (var participant in participants)
                await Clients.User(Convert.ToString(participant.UserId)).ReceiveMessage(message, $"#{user} send: {message}", DateTime.Now);

            return true;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userID = Context.UserIdentifier;

            Console.WriteLine($"USER WITH ID: #{userID} HAS BEEN DISCONNECTED");

            return base.OnDisconnectedAsync(exception);
        }

        public async Task<ServiceResponse<MessageViewModel>> SendMessage(MessageSendViewModel model)
        {
            var userId = Context.UserIdentifier;

            var participants = await _conversationRepository.GetAll()
                .Where(c => c.Guid.Equals(model.ConversationGuid))
                .Select(c => c.Participants)
                .FirstOrDefaultAsync();

            var message = await _messageService.SendMessageAsync(model, userId);

            if (!message.IsSuccess) return message;

            foreach (var participant in participants)
            {
                string participantIdString = Convert.ToString(participant.UserId);

                await Clients.User(participantIdString).ReceiveMessage(message);
            }

            return message;
        }

        public Task SendConnectedUsers(string room)
        {
            var users = _connections.Values
                .Where(x => x.Room == room)
                .Select(x => x.User!);

            return Clients.Group(room).ReceiveConnectedUsers(users);
        }
    }
}
