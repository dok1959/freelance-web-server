using FreelanceWebServer.Models;
using FreelanceWebServer.Models.Views.Account;

namespace FreelanceWebServer.Services.AccountServices
{
    interface IAccountService
    {
        void Register(RegistrationViewModel model);
        User Authenticate(LoginViewModel model);
    }
}
