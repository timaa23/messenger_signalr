using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_messenger_signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet]
        [Route("get/conversationId/{conversationId}")]
        public async Task<IActionResult> GetByConversationIdAsync(int conversationId)
        {
            var userId = User.FindFirst("id").Value;

            var result = await _messageService.GetMessagesByConversationId(conversationId, userId);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendAsync([FromBody] MessageSendViewModel model)
        {
            var userId = User.FindFirst("id").Value;

            var result = await _messageService.SendMessageAsync(model, userId);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
