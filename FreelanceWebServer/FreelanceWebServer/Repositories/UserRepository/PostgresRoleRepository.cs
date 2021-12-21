using Dapper;
using FreelanceWebServer.Data.PostgreSQL;
using FreelanceWebServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceWebServer.Repositories.UserRepository
{
    public class PostgresRoleRepository : IRoleRepository
    {
        private readonly PostgresDBContext _context;

        public PostgresRoleRepository(PostgresDBContext context) => _context = context;

        public async Task<IEnumerable<Role>> GetAll()
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryAsync<Role>("SELECT * FROM \"roles\";");
        }

        public async Task<Role> Get(long id)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<Role>("SELECT * FROM \"roles\" WHERE id = @id;", new { id = id });
        }
    }
}
