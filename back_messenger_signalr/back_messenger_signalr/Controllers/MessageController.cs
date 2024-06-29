using back_messenger_signalr.Helpers;
using back_messenger_signalr.Models.Account;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Route("get/conversationGuid/{guid}")]
        public async Task<IActionResult> GetByConversationGuidAsync(Guid guid)
        {
            var userId = User.FindFirst("id").Value;

            var result = await _messageService.GetMessagesByConversationGuid(guid, userId);

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
