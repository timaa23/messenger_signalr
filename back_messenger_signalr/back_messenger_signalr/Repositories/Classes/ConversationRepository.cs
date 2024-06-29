using AutoMapper;
using AutoMapper.QueryableExtensions;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Repositories.Classes
{
    public class ConversationRepository : GenericRepository<ConversationEntity, int>, IConversationRepository
    {
        private readonly AppEFContext _dbContext;
        private readonly IMapper _mapper;
        public ConversationRepository(AppEFContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<ConversationEntity> ConversationsEager => GetAll()
            .Include(c => c.Messages)
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .AsNoTracking();

        public IQueryable<ConversationViewModel> GetConversationsByUserIdAsync(string userId)
        {
            int.TryParse(userId, out var id);

            var conversations = GetAll()
                .Where(c => c.Participants.Any(p => p.UserId.Equals(id)));

            var result = conversations
                .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return result;
        }

        public async Task<ConversationViewModel> GetConversationByGuidAsync(Guid conversationGuid, string userId)
        {
            var conversations = GetAll().Where(c => c.Guid.Equals(conversationGuid));

            if (!IsUserInConversation(conversations, userId))
            {
                throw new AccessViolationException("User is not member of this conversation.");
            }

            var result = conversations
                .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await result.FirstOrDefaultAsync();
        }

        public async Task<ConversationViewModel> CreateConversationAsync(IEnumerable<UserEntity> participants)
        {
            var conversationType = participants.Count() > 2 ? ConversationTypes.Group : ConversationTypes.Single;

            if (conversationType == ConversationTypes.Single &&
                IsConversationExistWithSameUsers(participants))
            {
                throw new ArgumentException("A conversation with these users already exists.");
            }

            var conversation = new ConversationEntity
            {
                Name = $"Created by {participants.FirstOrDefault().Name}",
                ConversationType = conversationType
            };

            await _dbContext.Conversations.AddAsync(conversation);

            foreach (var participant in participants)
            {
                var participantEntity = new ParticipantEntity
                {
                    User = participant,
                    Name = participant.Name,
                    Conversation = conversation
                };

                await _dbContext.Participants.AddAsync(participantEntity);
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

            return _mapper.Map<ConversationViewModel>(conversation);
        }

        private bool IsConversationExistWithSameUsers(IEnumerable<UserEntity> participants)
        {
            var userIdSet = new HashSet<int>(participants.Select(p => p.Id));

            return GetAll()
                .Where(c => c.ConversationType.Equals(ConversationTypes.Single))
                .Any(c => c.Participants.Count == userIdSet.Count &&
                          c.Participants.All(p => userIdSet.Contains(p.UserId)));
        }

        private bool IsUserInConversation(IQueryable<ConversationEntity> conversation, string userId)
        {
            int.TryParse(userId, out var id);

            return conversation.Any(c => c.Participants.Any(p => p.UserId == id));
        }
    }
}
