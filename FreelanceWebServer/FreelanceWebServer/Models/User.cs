namespace FreelanceWebServer.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string HashedPassword { get; set; }

        public long RoleId { get; set; }

        public string RefreshToken { get; set; }
    }
}