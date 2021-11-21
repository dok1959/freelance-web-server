using bc = BCrypt.Net.BCrypt;

namespace FreelanceWebServer.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => bc.HashPassword(password);

        public bool Verify(string password, string hashedPassword) => bc.Verify(password, hashedPassword);
    }
}
