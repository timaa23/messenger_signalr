using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public ConversationService(IConversationRepository conversationRepository, IParticipantRepository participantRepository, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _conversationRepository = conversationRepository;
            _participantRepository = participantRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<List<ConversationViewModel>>> GetConversationsByUserIdAsync(string userId)
        {
            int.TryParse(userId, out var userIdInt);

            int messagesAmount = 25;

            var result = await _conversationRepository.GetConversationsByUserIdAsync(userIdInt)
                .OrderByDescending(m => m.DateCreated)
                .Take(messagesAmount)
                .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return new()
            {
                Message = "Success",
                Payload = result
            };
        }

        public async Task<ServiceResponse<ConversationViewModel>> GetConversationByGuidAsync(Guid guid, string userId)
        {
            try
            {
                int.TryParse(userId, out var userIdInt);

                await ValidateConversation(guid, userIdInt);

                var result = await _conversationRepository.GetConversationByGuidAsync(guid)
                    .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

                return new()
                {
                    Message = "Success",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<ConversationViewModel>> CreateConversationAsync(ConversationCreateViewModel model)
        {
            try
            {
                await ValidateUsers(model.ParticipantIds);

                if (model.ConversationType == ConversationTypes.Single)
                    ValidateSingleConversation(model.ParticipantIds);

                var result = await _conversationRepository.CreateConversationAsync(model);

                var response = _mapper.Map<ConversationViewModel>(result);

                return new()
                {
                    Message = "Success",
                    Payload = response
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        private async Task ValidateConversation(Guid conversationGuid, int userId)
        {
            var conversation = await _conversationRepository.GetAll()
                .Include(c => c.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Guid.Equals(conversationGuid));


            if (conversation == null) throw new Exception("Conversation is undefined.");

            if (!conversation.Participants.Any(p => p.UserId.Equals(userId))) throw new AccessViolationException("User is not member of this conversation.");
        }

        private void ValidateSingleConversation(List<string> participants)
        {
            var userIdSet = ConvertStringsToHashSetInt(participants);

            var isExist = _conversationRepository.GetAll()
                .Where(c => c.ConversationType.Equals(ConversationTypes.Single))
                .Any(c => c.Participants.Count == userIdSet.Count &&
                          c.Participants.All(p => userIdSet.Contains(p.UserId)));

            if (isExist) throw new Exception("Conversation with these users already exists.");
        }

        private async Task ValidateUsers(List<string> participants)
        {
            foreach (var userId in participants)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) throw new Exception($"User with ID:{userId} doesn't exist.");
            }
        }

        private HashSet<int> ConvertStringsToHashSetInt(List<string> strings)
        {
            var idsArrayInt = Array.ConvertAll(strings.ToArray(), s => int.TryParse(s, out var i) ? i : -1);

            return new HashSet<int>(idsArrayInt);
        }
    }
}
