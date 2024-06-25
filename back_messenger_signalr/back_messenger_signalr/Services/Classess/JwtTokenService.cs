using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace back_messenger_signalr.Services.Classess
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;

        public JwtTokenService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(UserEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.UserName),
                new Claim("image", user.Image ?? String.Empty),
            };

            foreach (var item in roles)
                claims.Add(new Claim("roles", item));

            var key = _configuration.GetValue<string>("JwtKey");
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signInCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JwtIssuer"),
                signingCredentials: signInCredentials,
                expires: DateTime.Now.AddDays(7),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
