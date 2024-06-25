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
        public Guid Guid { get; set; } = Guid.NewGuid();

        public virtual ICollection<MessageEntity> Messages { get; set; }
        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}

//В мене є декілька табличок в asp net ef на c#: 
//    [Table("Conversations_tbl")]
//public class ConversationEntity : BaseEntity<int>
//{
//    public string Title { get; set; }
//    public Guid Guid { get; set; } = Guid.NewGuid();

//    public virtual ICollection<MessageEntity> Messages { get; set; }
//    public virtual ICollection<ParticipantEntity> Participants { get; set; }
//},
//    [Table("Participants_tbl")]
//public class ParticipantEntity : BaseEntity<int>
//{
//    public int ConversationId { get; set; }
//    public ConversationEntity Conversation { get; set; }

//    public int UserId { get; set; }
//    public UserEntity User { get; set; }
//},
//    public class UserEntity : IdentityUser<int>
//{
//    [StringLength(100), Required]
//    public string Name { get; set; }

//    [StringLength(255)]
//    public string Image { get; set; }

//    public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
//    public virtual ICollection<MessageEntity> Messages { get; set; }
//    public virtual ICollection<ParticipantEntity> Participants { get; set; }
//}.
//Як мені дістати всі Conversations за допомогою int userId?