namespace FreelanceWebServer.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string HashedPassword { get; set; }

        public Role Role { get; set; }

        public string RefreshToken { get; set; }
    }
}