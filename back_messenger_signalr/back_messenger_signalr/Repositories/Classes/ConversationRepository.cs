using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Models.Message;
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
            .ThenInclude(p => p.User)
            .AsNoTracking();

        public IQueryable<ConversationsViewModel> GetConversationsByUserIdAsync(string userId)
        {
            int.TryParse(userId, out var id);

            var conversations = Conversations
                .Where(c => c.Participants.Any(p => p.UserId == id))
                .Select(c => new ConversationsViewModel
                {
                    Guid = c.Guid,
                    Name = c.ConversationType == ConversationTypes.Group ? c.Name
                            : c.Participants.Where(p => p.UserId != id).Select(p => p.Name).FirstOrDefault(),
                    Image = c.Participants.Where(p => p.UserId != id).Select(p => p.User.Image).FirstOrDefault(),
                    LastMessage = c.Messages.OrderByDescending(m => m.DateCreated)
                    .Select(m => new MessageViewModel
                    {
                        Message = m.Body,
                        ConversationGuid = m.Conversation.Guid,
                        SenderId = m.SenderId,
                        MessageType = m.MessageType,
                        DateTime = m.DateCreated
                    })
                    .Take(1)
                    .FirstOrDefault()
                })
                .AsNoTracking();

            return conversations;
        }

        public async Task<ConversationEntity> CreateConversationAsync(IEnumerable<UserEntity> participants)
        {
            var conversationType = participants.Count() > 2 ? ConversationTypes.Group : ConversationTypes.Single;

            var conversation = new ConversationEntity
            {
                Name = $"Created by {participants.FirstOrDefault().Name}",
                ConversationType = conversationType
            };

            try
            {
                await InsertAsync(conversation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            foreach (var participant in participants)
            {
                var participantEntity = new ParticipantEntity
                {
                    Conversation = conversation,
                    User = participant,
                    Name = participant.Name
                };

                await _dbContext.Set<ParticipantEntity>().AddAsync(participantEntity);
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return conversation;
        }

        public async Task<ConversationEntity> GetConversationByGuidAsync(Guid conversationGuid)
        {
            return await Conversations.FirstOrDefaultAsync(m => m.Guid.Equals(conversationGuid));
        }
    }
}
