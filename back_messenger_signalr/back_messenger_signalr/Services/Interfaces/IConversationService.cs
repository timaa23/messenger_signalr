using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;

namespace back_messenger_signalr.Services.Interfaces
{
    public interface IConversationService
    {
        Task<ServiceResponse<List<ConversationViewModel>>> GetConversationsByUserIdAsync(string userId);
        Task<ServiceResponse<ConversationViewModel>> GetConversationByGuidAsync(Guid guid, string userId);
        Task<ServiceResponse<ConversationViewModel>> CreateConversationAsync(ConversationCreateViewModel model);
    }
}
