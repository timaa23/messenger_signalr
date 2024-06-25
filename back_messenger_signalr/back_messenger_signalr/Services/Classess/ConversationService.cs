using back_messenger_signalr.Entities;
using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;

namespace back_messenger_signalr.Services.Classess
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IParticipantRepository _participantRepository;
        public ConversationService(IConversationRepository conversationRepository, IParticipantRepository participantRepository)
        {
            _conversationRepository = conversationRepository;
            _participantRepository = participantRepository;
        }


        public async Task<ServiceResponse> GetConversationByGuidAsync(Guid guid)
        {
            var result = await _conversationRepository.GetConversationByGuid(guid);

            return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
        }

        public async Task<ServiceResponse> CreateConversationAsync(UserEntity creator, UserEntity participant)
        {
            var newConversation = new ConversationEntity
            {
                Name = $"{creator.Name} and {participant.Name}",
                Title = "Chat"
            };

            try
            {
                await _conversationRepository.InsertAsync(newConversation);
            }
            catch (Exception)
            {
                return new ServiceResponse { IsSuccess = false, Message = "Conversation creating error" };
            }

            var firstParticipant = new ParticipantEntity
            {
                Conversation = newConversation,
                User = creator
            };
            var secondParticipant = new ParticipantEntity
            {
                Conversation = newConversation,
                User = participant
            };

            try
            {
                await _participantRepository.InsertAsync(firstParticipant);
                await _participantRepository.InsertAsync(secondParticipant);
            }
            catch (Exception)
            {
                return new ServiceResponse { IsSuccess = false, Message = "Participant adding error" };
            }

            var result = await _conversationRepository.GetConversationByGuid(newConversation.Guid);

            return new ServiceResponse { IsSuccess = true, Message = "Success", Payload = result };
        }
    }
}
