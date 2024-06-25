using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace back_messenger_signalr.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(100), Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        public virtual ICollection<MessageEntity> Messages { get; set; }
        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
