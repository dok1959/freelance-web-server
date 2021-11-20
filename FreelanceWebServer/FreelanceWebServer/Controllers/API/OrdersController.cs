using FreelanceWebServer.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController() { }

        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => Ok();

        [Authorize]
        [HttpPut]
        public IActionResult Update(OrderDTO order) => Ok();
        
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) => Ok();
    }
}
