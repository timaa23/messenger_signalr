using AutoMapper;
using AutoMapper.QueryableExtensions;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Repositories.Interfaces;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Services.Classess
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository messageRepository, IConversationRepository conversationRepository, IUserService userService, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<MessageViewModel>> SendMessageAsync(MessageSendViewModel model, string userId)
        {
            try
            {
                int.TryParse(userId, out var userIdInt);

                var conversation = await ValidateConversation(model.ConversationGuid, userId);

                model.ConversationId = conversation.Id;
                var result = await _messageRepository.SendMessage(model, userIdInt);

                var response = _mapper.Map<MessageViewModel>(result,
                    opts => opts.AfterMap((dest, opt) => opt.ConversationGuid = model.ConversationGuid));

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

        public async Task<ServiceResponse<List<MessageViewModel>>> GetMessagesByConversationGuid(Guid conversationGuid, string userId, int last = 0)
        {
            try
            {
                await ValidateConversation(conversationGuid, userId);

                int messagesAmount = last > 0 ? last : 25;

                var result = await _messageRepository.GetMessagesByConversationGuid(conversationGuid)
                    .OrderByDescending(m => m.DateCreated)
                    .Take(messagesAmount)
                    .ProjectTo<MessageViewModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

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

        private async Task<ConversationEntity> ValidateConversation(Guid conversationGuid, string userId)
        {
            var conversation = await _conversationRepository.GetConversationByGuidAsync(conversationGuid)
                .SingleOrDefaultAsync();

            if (conversation == null) throw new Exception("Conversation is undefined.");

            bool hasConversation = await _userService.HasConversationAsync(userId, conversationGuid);
            if (!hasConversation) throw new AccessViolationException("User is not member of this conversation.");

            return conversation;
        }
    }
}