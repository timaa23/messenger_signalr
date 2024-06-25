using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Services.Classess
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly UserManager<UserEntity> _userManager;

        public ConversationService(IConversationRepository conversationRepository, IParticipantRepository participantRepository, UserManager<UserEntity> userManager)
        {
            _conversationRepository = conversationRepository;
            _participantRepository = participantRepository;
            _userManager = userManager;
        }

        public async Task<ServiceResponse> GetConversationsByUserIdAsync(string userId)
        {
            var result = await _conversationRepository.GetConversationsByUserIdAsync(userId).ToListAsync();

            return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
        }

        public async Task<ServiceResponse> GetConversationByGuidAsync(Guid guid)
        {
            var result = await _conversationRepository.GetConversationByGuidAsync(guid);

            return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
        }

        public async Task<ServiceResponse> CreateConversationAsync(ConversationCreateViewModel model)
        {
            var creator = await _userManager.FindByIdAsync(model.CreatorId);
            var participant = await _userManager.FindByIdAsync(model.ParticipantId);

            if (creator == null || participant == null)
            {
                return new ServiceResponse { IsSuccess = false, Message = "Undefined users" };
            }

            var participants = new List<UserEntity>(new[] { creator, participant });

            try
            {
                var result = await _conversationRepository.CreateConversationAsync(participants);
                return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
            }
            catch (Exception ex)
            {
                return new ServiceResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }
    }
}
