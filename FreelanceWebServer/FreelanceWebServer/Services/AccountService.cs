using AutoMapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models.DTO.Account;
using System.Threading.Tasks;

namespace FreelanceWebServer.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;

        public AccountService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Authenticate(LoginDTO model)
        {
            User user = null;

            if (model.Username != null)
            {
                user = await _userRepository.GetByUsername(model.Username);
            }
            else if (model.PhoneNumber != null)
            {
                user = await _userRepository.GetByPhoneNumber(model.PhoneNumber);
            }

            if(user == null || !_passwordHasher.Verify(model.Password, user.HashedPassword))
                return null;

            return user;
        }

        public async Task Register(RegistrationDTO model)
        {
            var user = _mapper.Map<User>(model);
            user.HashedPassword = _passwordHasher.Hash(model.Password);
            user.Role = Role.User;
            await _userRepository.Add(user);
        }

        public async Task ResetPassword(string password) => await new Task(() => { });

        public async Task<bool> TryVerifyPhoneNumber(string code) => await new Task<bool>(() => true);
    }
}
