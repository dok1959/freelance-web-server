using FreelanceWebServer.Models.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }

        public IActionResult Login([FromBody] LoginViewModel viewModel)
        {
            return Ok();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }

        public IActionResult Register([FromBody] RegistrationViewModel viewModel)
        {
            return Ok();
        }

        public IActionResult RefreshToken([FromBody] string token)
        {
            return Ok();
        }
    }
}
