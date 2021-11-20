using FreelanceWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceWebServer.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly List<User> _context;

        public MemoryUserRepository() => _context = new List<User>();

        public void Add(User user)
        {
            _context.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.ToList();
        }

        public User GetById(long id)
        {
            return _context.ToList().Find(u => u.Id.Equals(id));
        }

        public void Update(User user)
        {
            _context.RemoveAll(u => u.Id.Equals(user.Id));
            _context.Add(user);
        }

        public IEnumerable<User> Where(Func<User, bool> predicate)
        {
            return _context.Where(predicate).ToList();
        }

        public void Delete(User user)
        {
            _context.RemoveAll(u => u.Id.Equals(user.Id));
        }
    }
}
