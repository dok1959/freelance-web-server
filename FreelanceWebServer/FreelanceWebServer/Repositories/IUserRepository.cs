using FreelanceWebServer.Models;
using System;
using System.Collections.Generic;

namespace FreelanceWebServer.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User GetById(long id);
        IEnumerable<User> Where(Func<User, bool> predicate);
        void Update(User user);
        void Delete(User user);
    }
}
