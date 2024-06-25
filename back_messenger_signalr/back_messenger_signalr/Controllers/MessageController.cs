using back_messenger_signalr.Models.Account;
using back_messenger_signalr.Models.Message;
using back_messenger_signalr.Services.Interfaces;
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

        [HttpGet]
        [Route("get/conversation/{guid}")]
        public async Task<IActionResult> GetByConversationGuidAsync(Guid guid)
        {
            var result = await _messageService.GetMessagesByConversationGuid(guid);
            return SendResponse(result);
        }
        
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendAsync([FromBody] MessageSendViewModel model)
        {
            var result = await _messageService.SendMessage(model);
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
