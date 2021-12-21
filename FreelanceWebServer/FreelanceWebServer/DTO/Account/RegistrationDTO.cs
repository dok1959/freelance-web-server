using System.ComponentModel.DataAnnotations;

namespace FreelanceWebServer.Models.DTO.Account
{
    public class RegistrationDTO
    {
        public string Username { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
