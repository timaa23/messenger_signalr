using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Conversation;
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

        public IQueryable<ConversationEntity> ConversationsEager => GetAll()
            .Include(c => c.Messages)
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .AsNoTracking();

        public IQueryable<ConversationEntity> GetConversationsByUserIdAsync(int userId)
        {
            var result = GetAll()
                .Where(c => c.Participants.Any(p => p.UserId.Equals(userId)))
                .AsNoTracking();

            return result;
        }

        public IQueryable<ConversationEntity> GetConversationByGuidAsync(Guid conversationGuid)
        {
            return GetAll().Where(c => c.Equals(conversationGuid)).AsNoTracking();
        }

        public async Task<ConversationEntity> CreateConversationAsync(ConversationCreateViewModel model)
        {
            try
            {
                var conversation = new ConversationEntity
                {
                    Name = model.Name,
                    ConversationType = model.ConversationType,
                };

                await _dbContext.Conversations.AddAsync(conversation);

                foreach (var participant in model.ParticipantIds)
                {
                    int.TryParse(participant, out var participantId);

                    var participantEntity = new ParticipantEntity
                    {
                        UserId = participantId,
                    };

                    conversation.Participants.Add(participantEntity);
                }

                await _dbContext.SaveChangesAsync();

                return conversation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
