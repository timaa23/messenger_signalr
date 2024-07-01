using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Models.Conversation
{
    public class ConversationCreateViewModel
    {
        public string Name { get; set; } = null!;
        public List<string> ParticipantIds { get; set; }
        public ConversationTypes ConversationType { get; set; }
    }
}
