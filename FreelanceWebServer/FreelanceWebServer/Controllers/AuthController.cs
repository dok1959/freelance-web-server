using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FreelanceWebServer.Models;
using FreelanceWebServer.Services;
using FreelanceWebServer.Services.JWT;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

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
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Login DTO not valid");

            User user = await _accountService.Authenticate(model);

            if(user == null)
                return BadRequest("Wrong user credentials");

            var generatedToken = _refreshTokenGenerator.Generate();
            await _userRepository.UpdateRefreshToken(user.Id, generatedToken);

            return Ok(new UserTokensDTO 
            { 
                AccessToken = _accessTokenGenerator.Generate(user),
                RefreshToken = generatedToken
            });
        }

        /// <summary>
        /// Registrate account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            User user = null;

            if (model.Username != null)
            {
                user = await _userRepository.GetByUsername(model.Username);
            }
            else if (model.PhoneNumber != null)
            {
                user = await _userRepository.GetByPhoneNumber(model.PhoneNumber);
            }

            if (user != null)
                return BadRequest("User with this credentials is already registered");

            await _accountService.Register(model);

            return Ok();
        }

        /// <summary>
        /// Refresh user tokens
        /// </summary>
        /// <param name="token">Refresh token</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(UserTokensDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            if (token == null)
                return BadRequest("Wrong refresh token");

            bool isRefreshTokenValid = _refreshTokenValidator.Validate(token);
            if (!isRefreshTokenValid)
            {
                return BadRequest(new { errorMessage = "Invalid refresh token" });
            }

            User user = await _userRepository.GetByRefreshToken(token);

            if (user == null)
            {
                return BadRequest(new { errorMessage = "User with this token not found" });
            }

            var generatedToken = _refreshTokenGenerator.Generate();
            await _userRepository.UpdateRefreshToken(user.Id, generatedToken);

            return Ok(new UserTokensDTO
            {
                AccessToken = _accessTokenGenerator.Generate(user),
                RefreshToken = generatedToken
            });
        }
    }
}