namespace FreelanceWebServer.Services.PasswordHashers
{
    interface IPasswordHasher
    {
        string Hash();
        string Verify(string password, string hashedPassword);
    }
}
