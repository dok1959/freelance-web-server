using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer.Services
{
    public class AccountService : IAccountService
    {
        public User Authenticate(LoginDTO model)
        {
            return new User();
        }

        public void Register(RegistrationDTO model)
        {
            return;
        }
    }
}
