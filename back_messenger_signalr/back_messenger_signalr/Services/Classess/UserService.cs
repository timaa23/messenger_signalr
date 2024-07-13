using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace back_messenger_signalr.Services.Classess
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsUserExistsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null;
        }

        public async Task<bool> HasConversationAsync(string userId, int conversationId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new Exception("User is undefined");

            bool result = _userManager.Users.Where(u => u.Id.Equals(user.Id))
                .SelectMany(u => u.Participants)
                .Any(p => p.ConversationId.Equals(conversationId));

            return result;
        }
    }
}
