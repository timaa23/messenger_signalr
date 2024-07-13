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

        public IQueryable<MessageEntity> MessagesEager => GetAll()
            .Include(m => m.Sender)
            .Include(m => m.Conversation)
            .ThenInclude(c => c.Participants)
            .AsNoTracking();

        //public IQueryable<MessageEntity> GetMessagesByConversationGuid(Guid conversationGuid)
        //{
        //    var messages = GetAll()
        //        .Where(m => m.Conversation.Guid.Equals(conversationGuid))
        //        .AsNoTracking();

        //    return messages;
        //}

        public async Task<MessageEntity> SendMessage(MessageSendViewModel model, int userId)
        {
            var message = new MessageEntity
            {
                SenderId = userId,
                ConversationId = model.ConversationId,
                Body = model.Message,
                MessageType = model.MessageType,
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
