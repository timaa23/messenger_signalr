using System.ComponentModel.DataAnnotations.Schema;

namespace back_messenger_signalr.Entities
{
    [Table("Conversations_tbl")]
    public class ConversationEntity : BaseEntity<int>
    {
        public string Title { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();

        public virtual ICollection<MessageEntity> Messages { get; set; }
        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
