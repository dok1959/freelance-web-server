using FreelanceWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceWebServer.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        private long _idCounter = 1;

        public void Add(User user)
        {
            user.Id = _idCounter++;
            _users.Add(user);
        }

        public IEnumerable<User> GetAll() => _users.ToList();

        public User GetById(long id) => _users.ToList().Find(u => u.Id.Equals(id));

        public User Find(Func<User, bool> predicate) => _users.Find(new Predicate<User>(predicate));

        public IEnumerable<User> FindAll(Func<User, bool> predicate) => _users.Where(predicate).ToList();

        public void Update(User user)
        {
            _users.RemoveAll(u => u.Id.Equals(user.Id));
            _users.Add(user);
        }

        public void Delete(User user) => _users.RemoveAll(u => u.Id.Equals(user.Id));
    }
}
