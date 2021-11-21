using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        public ProfilesController() { }

        [Authorize]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(string id) => Ok();

        [Authorize]
        [HttpPut]
        public IActionResult Put() => Ok();

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) => Ok();
    }
}
