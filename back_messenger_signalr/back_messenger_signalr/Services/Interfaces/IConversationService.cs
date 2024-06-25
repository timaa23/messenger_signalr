using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IConversationService
    {
        Task<ServiceResponse> GetConversationsByUserIdAsync(string userId);
        Task<ServiceResponse> GetConversationByGuidAsync(Guid guid);
        Task<ServiceResponse> CreateConversationAsync(ConversationCreateViewModel model);
    }
}
