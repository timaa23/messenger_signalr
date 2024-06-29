using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Services.Classess
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<ServiceResponse<MessageViewModel>> SendMessageAsync(MessageSendViewModel model, string userId)
        {
            try
            {
                var result = await _messageRepository.SendMessage(model, userId);

                return new()
                {
                    Message = "Success",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<List<MessageViewModel>>> GetMessagesByConversationGuid(Guid conversationGuid, string userId, int last = 0)
        {
            int amount_messages = last > 0 ? last : 20;

            try
            {
                var result = await _messageRepository.GetMessagesByConversationGuid(conversationGuid, userId)
                    .Take(amount_messages)
                    .ToListAsync();

                return new()
                {
                    Message = "Success",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
