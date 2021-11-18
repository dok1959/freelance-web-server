using FreelanceWebServer.Models;
using System.Collections.Generic;
using FreelanceWebServer.Databases.PostgreSQL;

namespace FreelanceWebServer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly PostgreSQLContext _context;

        public UserRepository(PostgreSQLContext context) => _context = context;

        public void Add(User item)
        {
            
        }

        public IEnumerable<User> GetAll()
        {
            return new List<User>();
        }

        public User GetById(long id)
        {
            return new User();
        }

        public IEnumerable<User> Where()
        {
            return GetAll();
        }

        public void Update(User item)
        {
            
        }

        public void Delete(User item)
        {
            
        }
    }
}
