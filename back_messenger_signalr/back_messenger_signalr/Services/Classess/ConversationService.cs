using AutoMapper;
using AutoMapper.QueryableExtensions;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Services.Classess
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ConversationService(IConversationRepository conversationRepository, IParticipantRepository participantRepository, IUserService userService, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _participantRepository = participantRepository;
            _userService = userService;
            _mapper = mapper;
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
                await ValidateConversation(guid, userId);

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

        private async Task ValidateConversation(Guid conversationGuid, string userId)
        {
            var conversation = await _conversationRepository.GetConversationByGuidAsync(conversationGuid)
                .SingleOrDefaultAsync();

            if (conversation == null) throw new Exception("Conversation is undefined.");

            bool hasConversation = await _userService.HasConversationAsync(userId, conversationGuid);
            if (!hasConversation) throw new AccessViolationException("User is not member of this conversation.");
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
                bool isExists = await _userService.IsUserExistsAsync(userId);

                if (!isExists) throw new Exception($"User with ID:{userId} doesn't exist.");
            }
        }

        private HashSet<int> ConvertStringsToHashSetInt(List<string> strings)
        {
            var idsArrayInt = Array.ConvertAll(strings.ToArray(), s => int.TryParse(s, out var i) ? i : -1);

            return new HashSet<int>(idsArrayInt);
        }
    }
}
