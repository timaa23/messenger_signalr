using back_messenger_signalr.Entities;
using back_messenger_signalr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Repositories.Classes
{
    public class MessageRepository : GenericRepository<MessageEntity, int>, IMessageRepository
    {
        private readonly AppEFContext _dbContext;
        public MessageRepository(AppEFContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<MessageEntity> Messages => GetAll()
            .Include(m => m.Sender)
            .Include(m => m.Conversation)
            .AsNoTracking();

        public IQueryable<MessageEntity> GetMessagesByConversationGuid(string conversationGuid)
        {
            return Messages.Where(m => m.Conversation.Guid.Equals(conversationGuid));
        }
    }
}
