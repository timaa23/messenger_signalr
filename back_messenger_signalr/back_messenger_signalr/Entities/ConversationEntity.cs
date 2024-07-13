using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_messenger_signalr.Entities
{
    public enum ConversationTypes
    {
        Single,
        Group,
    }

    [Table("Conversations_tbl")]
    public class ConversationEntity : BaseEntity<int>
    {
        public ConversationTypes ConversationType { get; set; }
        //public Guid Guid { get; set; } = Guid.NewGuid();

        [StringLength(255)]
        public string Image { get; set; }

        public virtual ICollection<MessageEntity> Messages { get; set; }
        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}