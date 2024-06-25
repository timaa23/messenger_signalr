using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<MessageEntity, int>
    {
        IQueryable<MessageEntity> Messages { get; }
        IQueryable<MessageEntity> GetMessagesByConversationGuid(string conversationGuid);
    }
}
