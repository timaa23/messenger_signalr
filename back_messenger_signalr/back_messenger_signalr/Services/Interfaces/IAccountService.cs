using back_messenger_signalr.Models.Account;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginAsync(LoginViewModel model);
        public Task<ServiceResponse> RegistrationAsync(RegistrationViewModel model);
    }
}
