using AutoMapper;
using AutoMapper.QueryableExtensions;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Repositories.Classes
{
    public class MessageRepository : GenericRepository<MessageEntity, int>, IMessageRepository
    {
        private readonly AppEFContext _dbContext;
        private readonly IMapper _mapper;
        public MessageRepository(AppEFContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<MessageEntity> MessagesEager => GetAll()
            .Include(m => m.Sender)
            .Include(m => m.Conversation)
            .ThenInclude(c => c.Participants)
            .AsNoTracking();

        public IQueryable<MessageViewModel> GetMessagesByConversationGuid(Guid conversationGuid, string userId)
        {
            var messages = GetAll().Where(m => m.Conversation.Guid.Equals(conversationGuid));

            if (!IsUserInConversation(messages, userId))
            {
                throw new AccessViolationException("User is not member of this conversation.");
            }

            var result = messages
                .OrderByDescending(m => m.DateCreated)
                .ProjectTo<MessageViewModel>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return result;
        }

        public async Task<MessageViewModel> SendMessage(MessageSendViewModel model, string userId)
        {
            var conversation = await _dbContext.Conversations
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Participants)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Guid.Equals(model.ConversationGuid));

            int.TryParse(userId, out var userIdNum);

            if (conversation == null) throw new Exception("Conversation is undefined.");

            if (!conversation.Participants.Any(p => p.UserId.Equals(userIdNum))) throw new Exception("User is not member of this conversation.");

            var message = new MessageEntity
            {
                SenderId = userIdNum,
                ConversationId = conversation.Id,
                Body = model.Message,
                MessageType = model.MessageType,
                Name = "Message"
            };

            try
            {
                await InsertAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return _mapper.Map<MessageViewModel>(message,
                opts => opts.AfterMap((dest, opt) => opt.ConversationGuid = model.ConversationGuid));
        }

        private bool IsUserInConversation(IQueryable<MessageEntity> messages, string userId)
        {
            int.TryParse(userId, out var id);

            return messages.Select(m => m.Conversation).Any(c => c.Participants.Any(p => p.UserId == id));
        }
    }
}
