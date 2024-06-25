using back_messenger_signalr.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_messenger_signalr.Entities
{
    public enum MessageTypes
    {
        Text,
        File,
        Image,
        Video
    }

    [Table("Messages_tbl")]
    public class MessageEntity : BaseEntity<int>
    {
        public int SenderId { get; set; }
        public UserEntity Sender { get; set; }

        public int ConversationId { get; set; }
        public ConversationEntity Conversation { get; set; }

        public string Body { get; set; }
        public MessageTypes MessageType { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
