using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Services.Classess;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace back_messenger_signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConversationService _conversationService;
        public ConversationController(UserManager<UserEntity> userManager, IConversationService conversationService)
        {
            _userManager = userManager;
            _conversationService = conversationService;
        }

        [HttpGet]
        [Route("get/guid/{guid}")]
        public async Task<IActionResult> GetByGuidAsync(Guid guid)
        {
            var result = await _conversationService.GetConversationByGuidAsync(guid);
            return SendResponse(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] ConversationCreateViewModel model)
        {
            var user1 = await _userManager.FindByIdAsync(model.CreatorId);
            var user2 = await _userManager.FindByIdAsync(model.ParticipantId);

            var result = await _conversationService.CreateConversationAsync(user1, user2);
            return SendResponse(result);
        }

        private IActionResult SendResponse(ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
