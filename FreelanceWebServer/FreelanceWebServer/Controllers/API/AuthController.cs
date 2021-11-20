using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Services;
using FreelanceWebServer.Services.JWT;
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
        private IUserRepository _userRepository;
        private AccessTokenGenerator _accessTokenGenerator;
        private RefreshTokenGenerator _refreshTokenGenerator;
        private RefreshTokenValidator _refreshTokenValidator;

        public AuthController(
            IAccountService accountService,
            IUserRepository userRepository,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            RefreshTokenValidator refreshTokenValidator) 
        { 
            _accountService = accountService;
            _userRepository = userRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
        }

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

            user.RefreshToken = _refreshTokenGenerator.Generate();
            _userRepository.Update(user);

            return Ok(new UserTokensDTO 
            { 
                AccessToken = _accessTokenGenerator.Generate(user),
                RefreshToken = user.RefreshToken
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
            if (_userRepository.Find(u => u.Username == model.Username || u.PhoneNumber == model.PhoneNumber) != null)
            {
                return BadRequest(new { errorMessage = "User with this credentials is already registered" });
            }
            _accountService.Register(model);

            return Ok();
        }

        /// <summary>
        /// Refresh user tokens
        /// </summary>
        /// <param name="token">Refresh token</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(UserTokensDTO), StatusCodes.Status200OK)]
        public IActionResult RefreshToken([FromBody] string token)
        {
            if (token == null)
                return BadRequest("Wrong refresh token");

            bool isRefreshTokenValid = _refreshTokenValidator.Validate(token);
            if (!isRefreshTokenValid)
            {
                return BadRequest(new { errorMessage = "Invalid refresh token" });
            }

            var user = _userRepository.Find(u => u.RefreshToken == token);

            if (user == null)
            {
                return BadRequest(new { errorMessage = "User with this token not found" });
            }

            user.RefreshToken = _refreshTokenGenerator.Generate();
            _userRepository.Update(user);

            return Ok(new UserTokensDTO
            {
                AccessToken = _accessTokenGenerator.Generate(user),
                RefreshToken = user.RefreshToken
            });
        }
    }
}