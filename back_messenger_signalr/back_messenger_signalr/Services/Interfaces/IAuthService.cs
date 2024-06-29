using back_messenger_signalr.Models.Account;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<string>> LoginAsync(LoginViewModel model);
        public Task<ServiceResponse<string>> RegisterAsync(RegisterViewModel model);
    }
}
