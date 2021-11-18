using Microsoft.AspNetCore.Mvc;

namespace FreelanceWebServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
