using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetById(long id);
        Role GetByName(string name);
    }
}
