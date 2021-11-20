namespace FreelanceWebServer.Models.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            Surname = user.Surname;
            Name = user.Name;
        }
    }
}
