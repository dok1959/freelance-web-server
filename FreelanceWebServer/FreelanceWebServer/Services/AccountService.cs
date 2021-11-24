using AutoMapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models.DTO.Account;

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

        public User Authenticate(LoginDTO model)
        {
            User user = _userRepository.Find(u => u.Username == model?.Username || u.PhoneNumber == model?.PhoneNumber);

            if(user == null || !_passwordHasher.Verify(model.Password, user.HashedPassword))
                return null;

            return user;
        }

        public void Register(RegistrationDTO model)
        {
            var user = _mapper.Map<User>(model);
            user.HashedPassword = _passwordHasher.Hash(model.Password);
            _userRepository.Add(user);
        }

        public void ResetPassword(string password) { }

        public bool TryVerifyPhoneNumber(string code) => true;
    }
}
