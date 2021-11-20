using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;
using FreelanceWebServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers.API
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAccountService _accountService;

        public AuthController(IAccountService accountService) => _accountService = accountService;

        /// <summary>
        /// Sign in account
        /// </summary>
        /// <param name="model">Login DTO</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid user credentials</response>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserTokensDTO), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            User user = _accountService.Authenticate(model);

            if(user == null) 
                return BadRequest("Wrong user credentials");

            return Ok(new UserTokensDTO 
            { 
                AccessToken = "valid accessToken",
                RefreshToken = "valid refreshToken"
            });
        }

        /// <summary>
        /// Sign out account
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("logout")]
        public IActionResult Logout() => Ok();

        /// <summary>
        /// Registrate account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationDTO model)
        {
            _accountService.Register(model);

            return Ok();
        }

        /// <summary>
        /// Refresh user token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string token)
        {
            return Ok(new UserTokensDTO
            {
                AccessToken = "valid accessToken",
                RefreshToken = "valid refreshToken"
            });
        }
    }
}
