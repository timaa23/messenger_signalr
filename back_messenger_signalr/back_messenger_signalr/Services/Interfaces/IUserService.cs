namespace back_messenger_signalr.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> IsUserExistsAsync(string userId);
        public Task<bool> HasConversationAsync(string userId, int conversationId);
        public Task<bool> HasConversationAsync(string userId, Guid conversationGuid);
    }
}
