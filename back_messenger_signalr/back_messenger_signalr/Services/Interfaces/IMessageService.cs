using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ServiceResponse<MessageViewModel>> SendMessageAsync(MessageSendViewModel model, string userId);
        public Task<ServiceResponse<List<MessageViewModel>>> GetMessagesByConversationGuid(Guid conversationGuid, string userId, int last = 0);
    }
}
