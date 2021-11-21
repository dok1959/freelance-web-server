using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer.Services
{
    public interface IAccountService
    {
        User Authenticate(LoginDTO model);
        void Register(RegistrationDTO model, long roleId);
    }
}
