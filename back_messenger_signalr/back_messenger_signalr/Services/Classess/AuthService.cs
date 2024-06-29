using back_messenger_signalr.Constants;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Account;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace back_messenger_signalr.Services.Classess
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ()
                {
                    IsSuccess = false,
                    Message = "No user is associated with this email"
                };
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                return new ()
                {
                    IsSuccess = false,
                    Message = "Incorrect password"
                };
            }

            string token = await _jwtTokenService.CreateTokenAsync(user);
            return new ()
            {
                Message = "Success",
                Payload = token
            };
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterViewModel model)
        {
            UserEntity user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new ()
                {
                    IsSuccess = false,
                    Message = "Email has already used"
                };
            }

            user = new UserEntity
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.Name + "-" + Guid.NewGuid().ToString("N").Substring(0, 8)
            };

            var userCreate = await _userManager.CreateAsync(user, model.Password);
            if (!userCreate.Succeeded)
            {
                return new ()
                {
                    IsSuccess = false,
                    Message = "Registration failed"
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.User);
            string token = await _jwtTokenService.CreateTokenAsync(user);

            return new ()
            {
                Message = "Success",
                Payload = token
            };
        }
    }
}
