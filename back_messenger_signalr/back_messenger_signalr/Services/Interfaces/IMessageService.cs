namespace back_messenger_signalr.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ServiceResponse> GetMessagesByConversationGuid(string conversationGuid, int last = 0);
    }
}
