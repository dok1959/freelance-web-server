namespace FreelanceWebServer.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public UserDTO(User user)
        {
            Username = user.Username;
            Surname = user.Surname;
            Name = user.Name;
        }
    }
}
