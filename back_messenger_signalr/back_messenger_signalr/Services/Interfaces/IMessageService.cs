using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ServiceResponse> SendMessage(MessageSendViewModel model);
        public Task<ServiceResponse> GetMessagesByConversationGuid(Guid conversationGuid, int last = 0);
    }
}
