using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO;
using FreelanceWebServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FreelanceWebServer.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet]
        public IActionResult Index()
        {
            _userRepository.Add(new User
            {
                Name = "Виталий",
                Surname = "Внуков"
            });

            _userRepository.Add(new User
            {
                Name = "Николай",
                Surname = "Борис"
            });

            var usersDtos = new List<UserDTO>();
            foreach(var user in _userRepository.GetAll())
            {
                usersDtos.Add(new UserDTO(user));
            }

            return View(usersDtos);
        }
    }
}
