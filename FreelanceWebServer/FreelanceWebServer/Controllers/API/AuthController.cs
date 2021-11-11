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

        public IActionResult Login()
        {
            return Ok();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }

        public IActionResult Register()
        {
            return Ok();
        }

        public IActionResult RefreshToken()
        {
            return Ok();
        }
    }
}
