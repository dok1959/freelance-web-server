using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;
using FreelanceWebServer.Repositories;

namespace FreelanceWebServer.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;
        public AccountService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public User Authenticate(LoginDTO model)
        {
            User user = _userRepository.Find(u => u.Username == model.Username || u.PhoneNumber == model.PhoneNumber);

            if(user == null || !_passwordHasher.Verify(model.Password, user.HashedPassword))
            {
                return null;
            }

            return user;
        }

        public void Register(RegistrationDTO model)
        {
            User user = new User
            {
                Username = model.Username,
                Surname = model.Surname,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                HashedPassword = _passwordHasher.Hash(model.Password)
            };
            _userRepository.Add(user);
        }
    }
}
