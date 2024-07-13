using back_messenger_signalr.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using back_messenger_signalr.Entities;

namespace back_messenger_signalr
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) : base(options)
        { }

        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(u => new { u.UserId, u.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });


            // Relations
            builder.Entity<MessageEntity>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.Messages)
                .HasForeignKey(f => f.SenderId)
                .IsRequired();
            builder.Entity<MessageEntity>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(f => f.ConversationId)
                .IsRequired();

            builder.Entity<ParticipantEntity>()
                .HasOne(p => p.User)
                .WithMany(s => s.Participants)
                .HasForeignKey(f => f.UserId)
                .IsRequired();
            builder.Entity<ParticipantEntity>()
                .HasOne(p => p.Conversation)
                .WithMany(s => s.Participants)
                .HasForeignKey(f => f.ConversationId)
                .IsRequired();

            // Unique constraints 
            builder.Entity<UserEntity>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            builder.Entity<UserEntity>()
                .HasIndex(u => u.NormalizedUserName)
                .IsUnique();
            builder.Entity<MessageEntity>()
                .HasIndex(m => m.Guid)
                .IsUnique();

            // Other properties
            builder.Entity<MessageEntity>()
                .Property(m => m.MessageType)
                .HasConversion<string>();
            builder.Entity<ConversationEntity>()
                .Property(m => m.ConversationType)
                .HasConversion<string>();
        }
    }
}
