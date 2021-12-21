using FreelanceWebServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceWebServer.Repositories.UserRepository
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAll();

        public Task<Role> Get(long id);
    }
}
