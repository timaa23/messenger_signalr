using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Models.Participant;

namespace back_messenger_signalr.Models.Conversation
{
    public class ConversationViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public MessageViewModel LastMessage { get; set; }
        public ConversationTypes ConversationType { get; set; }
        public List<ParticipantViewModel> Participants { get; set; }
    }
}
