using back_messenger_signalr.Helpers;
using back_messenger_signalr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace back_messenger_signalr.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IDictionary<string, UserRoomConnection> _connections;
        private readonly OnlineDB _onlineDB;
        public ChatHub(IDictionary<string, UserRoomConnection> connections, OnlineDB onlineDB)
        {
            _connections = connections;
            _onlineDB = onlineDB;
        }

        public override Task OnConnectedAsync()
        {
            var userID = Context.UserIdentifier;

            Console.WriteLine($"USER WITH ID: #{userID} HAS BEEN CONNECTED");

            return base.OnConnectedAsync();
        }

        public async Task TestAsync(UserRoomConnection connection)
        {
            var user = Context.UserIdentifier;

            await Clients.All.ReceiveMessage(connection.User!, $"{connection.User} with userName {user}", DateTime.Now);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (!_connections.TryGetValue(Context.ConnectionId, out var user_room))
            {
                return base.OnDisconnectedAsync(exception);
            }

            Clients.Group(user_room.Room!).ReceiveMessage(user_room.User!, $"{user_room.User} has been disconnected", DateTime.Now);

            _connections.Remove(Context.ConnectionId);
            SendConnectedUsers(user_room.Room!);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var user_room))
            {
                await Clients.Group(user_room.Room!).ReceiveMessage(user_room.User!, message, DateTime.Now);
            }
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
