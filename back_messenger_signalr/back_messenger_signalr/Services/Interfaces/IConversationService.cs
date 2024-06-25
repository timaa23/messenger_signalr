using back_messenger_signalr.Entities.Identity;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IConversationService
    {
        Task<ServiceResponse> GetConversationByGuidAsync(Guid guid);
        Task<ServiceResponse> CreateConversationAsync(UserEntity creator, UserEntity participant);
    }
}
