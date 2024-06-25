using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IConversationRepository : IGenericRepository<ConversationEntity, int>
    {
        IQueryable<ConversationEntity> Conversations { get; }
        Task<ConversationEntity> GetConversationByGuid(Guid conversationGuid);
    }
}
