using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_messenger_signalr.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        [Route("get/user")]
        public async Task<IActionResult> GetUserConversationsAsync()
        {
            var userId = User.FindFirst("id").Value;

            var result = await _conversationService.GetConversationsByUserIdAsync(userId);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet]
        [Route("get/guid/{guid}")]
        public async Task<IActionResult> GetByGuidAsync(Guid guid)
        {
            var userId = User.FindFirst("id").Value;

            var result = await _conversationService.GetConversationByGuidAsync(guid, userId);

            if (result.Payload == null) return NotFound(result);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] ConversationCreateViewModel model)
        {
            var result = await _conversationService.CreateConversationAsync(model);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
