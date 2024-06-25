using back_messenger_signalr.Entities;
using back_messenger_signalr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Repositories.Classes
{
    public class ConversationRepository : GenericRepository<ConversationEntity, int>, IConversationRepository
    {
        private readonly AppEFContext _dbContext;
        public ConversationRepository(AppEFContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ConversationEntity> Conversations => GetAll()
            .Include(c => c.Messages)
            .Include(c => c.Participants)
            .AsNoTracking();

        public async Task<ConversationEntity> GetConversationByGuid(Guid conversationGuid)
        {
            return await Conversations.FirstOrDefaultAsync(m => m.Guid.Equals(conversationGuid));
        }
    }
}
