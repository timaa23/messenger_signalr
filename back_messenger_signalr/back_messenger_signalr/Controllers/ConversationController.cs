using back_messenger_signalr.Entities.Identity;
using back_messenger_signalr.Models.Conversation;
using back_messenger_signalr.Services.Classess;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [Route("get_user_conversations")]
        public async Task<IActionResult> GetUserConversationsAsync()
        {
            var userId = User.FindFirst("id").Value;

            var result = await _conversationService.GetConversationsByUserIdAsync(userId);
            return SendResponse(result);
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
            var result = await _conversationService.CreateConversationAsync(model);
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
