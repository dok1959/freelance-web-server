using FreelanceWebServer.Models.Views;
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
        public IActionResult Update(OrderViewModel viewModel) => Ok();
        
        [Authorize]
        public IActionResult Delete(long id) => Ok();
    }
}
