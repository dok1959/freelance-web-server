using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer.Services
{
    public interface IAccountService
    {
        void Register(RegistrationDTO model);
        User Authenticate(LoginDTO model);
    }
}
