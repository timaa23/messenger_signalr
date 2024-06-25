using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Models.Conversation
{
    public class ConversationsViewModel
    {
        public Guid Guid{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public MessageViewModel LastMessage { get; set; }
    }
    
    public class ConversationCreateViewModel
    {
        public string CreatorId { get; set; }
        public string ParticipantId { get; set; }
    }
}
