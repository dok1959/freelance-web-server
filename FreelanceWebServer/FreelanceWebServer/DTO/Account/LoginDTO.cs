using System.ComponentModel.DataAnnotations;

namespace FreelanceWebServer.Models.DTO.Account
{
    public class LoginDTO
    {
        public string Username { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
