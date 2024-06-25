using back_messenger_signalr.Models.Account;
using back_messenger_signalr.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back_messenger_signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            var result = await _accountService.LoginAsync(model);
            return SendResponse(result);
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationViewModel model)
        {
            var result = await _accountService.RegistrationAsync(model);
            return SendResponse(result);
        }

        [HttpGet]
        [Route("get_identity")]
        public async Task<IActionResult> GetIdentityAsync()
        {
            var id = User.FindFirst("id").Value;

            var result = new ServiceResponse
            {
                IsSuccess = true,
                Message = "Identity",
                Payload = id
            };
            return SendResponse(result);
        }

        // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQiLCJ1c2VyTmFtZSI6ImFkbWluIiwiaW1hZ2UiOiJodHRwczovL3d3dy5ncmF2YXRhci5jb20vYXZhdGFyLz9kPW1wIiwicm9sZXMiOiJhZG1pbiIsImV4cCI6MTcxOTk1NTE5MywiaXNzIjoiVGltYS0wMTYwIn0.MnHd0BH3QYL6nji_UQmobCd0cVJLsxRpjFFJm_SxIeY

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
