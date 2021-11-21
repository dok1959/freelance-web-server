using System;
using System.Linq;
using System.Collections.Generic;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Repositories
{
    public class MemoryRoleRepository : IRoleRepository
    {
        private readonly List<Role> _roles = new List<Role>
        {
            new Role
            {
                Id = 1,
                Name = "admin"
            },
            new Role
            {
                Id = 2,
                Name = "moderator"
            },
            new Role
            {
                Id = 3,
                Name = "customer"
            },
            new Role
            {
                Id = 4,
                Name = "employee"
            }
        };

        public IEnumerable<Role> GetAll() => _roles.ToList();

        public Role GetById(long id) => _roles.Find(r => r.Id.Equals(id));

        public Role GetByName(string name) => _roles.Find(r => r.Name.Equals(name));
    }
}
