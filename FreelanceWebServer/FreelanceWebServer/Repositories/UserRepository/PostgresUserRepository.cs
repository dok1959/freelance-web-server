using System;
using System.Collections.Generic;
using Dapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Data.PostgreSQL;

namespace FreelanceWebServer.Repositories
{
    public class PostgresUserRepository : IUserRepository
    {
        private readonly PostgresDBContext _context;

        public PostgresUserRepository(PostgresDBContext context) => _context = context;

        public void Add(User user)
        {
            _context.GetConnection().Execute(
                "INSERT INTO \"Users\" (username, surname, name, phoneNumber) Values (@Username, @Surname, @Name, @PhoneNumber);",
                new
                {
                    Username = user.Username,
                    Surname = user.Surname,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                });
        }

        public IEnumerable<User> GetAll()
        {
            return _context.GetConnection().Query<User>("SELECT * FROM \"Users\";");
        }

        public User GetById(long id)
        {
            return new User();
        }

        public User Find(Func<User, bool> predicate)
        {
            return new User();
        }

        public IEnumerable<User> FindAll(Func<User, bool> predicate)
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
