namespace FreelanceWebServer.Models
{
    public class User : BaseModel
    { 
        public string Surname { get; set; }

        public string Name { get; set; }

        public string HashedPassword { get; set; }
    }
}