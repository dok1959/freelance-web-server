namespace FreelanceWebServer.Services
{
    interface IPasswordHasher
    {
        string Hash();
        string Verify(string password, string hashedPassword);
    }
}
