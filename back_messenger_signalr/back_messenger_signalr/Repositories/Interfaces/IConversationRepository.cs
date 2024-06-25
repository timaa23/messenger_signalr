using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IConversationRepository : IGenericRepository<ConversationEntity, int>
    {
        IQueryable<ConversationEntity> Conversations { get; }
        IQueryable<ConversationsViewModel> GetConversationsByUserIdAsync(string userId);

        Task<ConversationEntity> CreateConversationAsync(IEnumerable<UserEntity> participants);
        Task<ConversationEntity> GetConversationByGuidAsync(Guid conversationGuid);
    }
}
