using back_messenger_signalr.Entities.Identity;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(UserEntity user);
    }
}
