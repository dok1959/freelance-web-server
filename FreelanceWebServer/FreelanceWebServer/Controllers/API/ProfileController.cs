using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {

        }

        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok();
        }

        [Authorize]
        public IActionResult Update()
        {
            return Ok();
        }

        [Authorize]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}
