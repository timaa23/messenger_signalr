using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Models.Message
{
    public class MessageSendViewModel
    {
        public string Message { get; set; }
        public int ConversationId { get; set; }
        public Guid ConversationGuid { get; set; }
        public MessageTypes MessageType { get; set; }
    }
}
