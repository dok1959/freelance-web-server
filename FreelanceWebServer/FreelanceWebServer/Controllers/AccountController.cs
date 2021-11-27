using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FreelanceWebServer.Services;

namespace FreelanceWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("password/reset")]
        [Authorize]
        public IActionResult ResetPassword([FromBody] string password) => Ok();

        /// <summary>
        /// Send verification code on phone number
        /// </summary>
        /// <returns></returns>
        [HttpGet("phone/sendVerificationCode")]
        [Authorize]
        public IActionResult SendVerificationCodeOnPhone() => Ok();

        /// <summary>
        /// Verificate phone number by code
        /// </summary>
        /// <param name="code">Verification code</param>
        /// <returns></returns>
        [HttpPut("phone/verificate")]
        [Authorize]
        public IActionResult VerifyPhoneNumber([FromBody] string code) => Ok();
    }
}
