using System.Threading.Tasks;
using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer.Services
{
    public interface IAccountService
    {
        Task<User> Authenticate(LoginDTO model);
        Task Register(RegistrationDTO model);
        Task ResetPassword(string password);
        Task<bool> TryVerifyPhoneNumber(string code);
    }
}
