using FreelanceWebServer.Models;
using System.Collections.Generic;
using FreelanceWebServer.Data.PostgreSQL;
using System;

namespace FreelanceWebServer.Repositories
{
    public class PostgresUserRepository : IUserRepository
    {
        private readonly PostgresDBContext _context;

        public PostgresUserRepository(PostgresDBContext context) => _context = context;

        public void Add(User item)
        {
            return;
        }

        public IEnumerable<User> GetAll()
        {
            return new List<User>();
        }

        public User GetById(long id)
        {
            return new User();
        }

        public IEnumerable<User> Where(Func<User, bool> predicate)
        {
            return new List<User>();
        }

        public void Update(User item)
        {
            return;
        }

        public void Delete(User item)
        {
            return;
        }
    }
}
