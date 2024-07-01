using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Models.Message
{
    public class MessageViewModel
    {
        //public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        //public int ConversationId { get; set; }
        public Guid ConversationGuid { get; set; }
        public MessageTypes MessageType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
