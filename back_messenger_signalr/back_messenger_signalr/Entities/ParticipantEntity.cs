using back_messenger_signalr.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_messenger_signalr.Entities
{
    [Table("Participants_tbl")]
    public class ParticipantEntity : BaseEntity<int>
    {
        public int ConversationId { get; set; }
        public ConversationEntity Conversation { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
