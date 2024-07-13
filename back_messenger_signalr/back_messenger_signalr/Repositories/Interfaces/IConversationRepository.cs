using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IConversationRepository : IGenericRepository<ConversationEntity, int>
    {
        IQueryable<ConversationEntity> ConversationsEager { get; }
        IQueryable<ConversationEntity> GetConversationsByUserIdAsync(int userId);
        //IQueryable<ConversationEntity> GetConversationByGuidAsync(Guid conversationGuid);

        Task<ConversationEntity> CreateConversationAsync(ConversationCreateViewModel model);
    }
}
