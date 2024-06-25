using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Models.Message
{
    public class MessageViewModel
    {
        public int SenderId { get; set; }
        public string Message { get; set; }
        public Guid ConversationGuid { get; set; }
        public MessageTypes MessageType { get; set; }
        public DateTime DateTime { get; set; }
    }
    
    public class MessageSendViewModel
    {
        public int SenderId { get; set; }
        public string Message { get; set; }
        public Guid ConversationGuid { get; set; }
        public MessageTypes MessageType { get; set; }
    }
}
