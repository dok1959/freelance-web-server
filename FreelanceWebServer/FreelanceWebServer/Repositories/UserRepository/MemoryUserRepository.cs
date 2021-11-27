using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        private long _idCounter = 1;

        public Task Add(User user)
        {
            user.Id = _idCounter++;
            _users.Add(user);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAll() 
            => await Task.FromResult(_users.ToList());

        public async Task<User> Get(long id) 
            => await Task.FromResult(_users.ToList().Find(u => u.Id.Equals(id)));

        public async Task<User> GetByUsername(string username) 
            => await Task.FromResult(_users.Find(u => u.Username == username));

        public async Task<User> GetByPhoneNumber(string phoneNumber) 
            => await Task.FromResult(_users.Find(u => u.PhoneNumber == phoneNumber));

        public async Task<User> GetByRefreshToken(string token) 
            => await Task.FromResult(_users.Find(u => u.RefreshToken == token));

        public Task Update(User user)
        {
            _users.RemoveAll(u => u.Id.Equals(user.Id));
            _users.Add(user);

            return Task.CompletedTask;
        }

        public Task UpdateRefreshToken(long id, string token)
        {
            var user = _users.Find(u => u.Id.Equals(id));
            user.RefreshToken = token;

            return Task.CompletedTask;
        }

        public async Task Delete(long id) 
            => await Task.FromResult(_users.RemoveAll(u => u.Id.Equals(id)));
    }
}
