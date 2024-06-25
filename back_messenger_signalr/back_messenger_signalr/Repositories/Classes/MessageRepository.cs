using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;
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

        public IQueryable<MessageViewModel> GetMessagesByConversationGuid(Guid conversationGuid)
        {
            return Messages.Where(m => m.Conversation.Guid.Equals(conversationGuid)).Select(m => new MessageViewModel
            {
                Message = m.Body,
                ConversationGuid = m.Conversation.Guid,
                SenderId = m.SenderId,
                MessageType = m.MessageType,
                DateTime = m.DateCreated
            }).AsNoTracking();
        }

        public async Task<MessageEntity> SendMessage(MessageSendViewModel model)
        {
            var conversation = await _dbContext.Set<ConversationEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Guid.Equals(model.ConversationGuid));

            if (conversation == null) throw new Exception("Conversation is undefined");

            var message = new MessageEntity
            {
                SenderId = model.SenderId,
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

            return message;
        }
    }
}
