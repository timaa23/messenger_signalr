using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Repositories.Classes;
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

        public async Task<ServiceResponse> SendMessage(MessageSendViewModel model)
        {
            try
            {
                var result = await _messageRepository.SendMessage(model);
                return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
            }
            catch (Exception ex)
            {
                return new ServiceResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ServiceResponse> GetMessagesByConversationGuid(Guid conversationGuid, int last = 0)
        {
            var result = await _messageRepository.GetMessagesByConversationGuid(conversationGuid).Take(last > 0 ? last : 10).ToListAsync();

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Success",
                Payload = result
            };
        }
    }
}
