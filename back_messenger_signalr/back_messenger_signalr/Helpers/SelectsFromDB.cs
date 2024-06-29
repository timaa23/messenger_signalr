using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Helpers
{
    public static class SelectsFromDB
    {
        public static IQueryable<MessageViewModel> SelectMessageViewModel(this IQueryable<MessageEntity> query)
        {
            var result = query.Select(m => new MessageViewModel
            {
                Guid = m.Guid,
                Message = m.Body,
                SenderId = m.SenderId,
                ConversationGuid = m.Conversation.Guid,
                MessageType = m.MessageType,
                DateTime = m.DateCreated
            });

            return result;
        }
    }
}
