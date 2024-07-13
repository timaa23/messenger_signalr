using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<MessageEntity, int>
    {
        IQueryable<MessageEntity> MessagesEager { get; }
        //IQueryable<MessageEntity> GetMessagesByConversationGuid(Guid conversationGuid);
        Task<MessageEntity> SendMessage(MessageSendViewModel model, int userId);
    }
}
