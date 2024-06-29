using back_messenger_signalr.Models.Account;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back_messenger_signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _accountService;
        public AuthController(IAuthService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            var result = await _accountService.LoginAsync(model);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            var result = await _accountService.RegisterAsync(model);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("get_identity")]
        public async Task<IActionResult> GetIdentityAsync()
        {
            var id = User.FindFirst("id").Value;

            ServiceResponse<string> result = new ()
            {
                Message = "Identity",
                Payload = id
            };
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
