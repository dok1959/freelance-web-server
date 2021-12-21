using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Data.PostgreSQL;

namespace FreelanceWebServer.Repositories.OrderRepository
{
    public class PostgresOrderRepository : IOrderRepository
    {
        private readonly PostgresDBContext _context;

        public PostgresOrderRepository(PostgresDBContext context) => _context = context;

        public async Task Add(Order order)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync(@"INSERT INTO orders (title, description, cost, customer_id, info_id, deadline, special_id) VALUES
                (@Title, @Description, @Cost, @CustomerId, @InfoId, @Deadline, @SpecialId);", order);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var connection = await _context.GetFreeConnection();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            return await connection.QueryAsync<Order>("SELECT * FROM \"orders\";");
        }

        public async Task<Order> Get(long id)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<Order>("SELECT * FROM \"orders\" WHERE id = @id;", new { id = id });
        }

        public async Task Update(Order order)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync(@"UPDATE orders SET
                title = @Title,
                description = @Description,
                contractor_id = @ContractorId,
                cost = @Cost,
                deadline = @Deadline,
                special_id = @SpecialId
                WHERE id = @Id;", order);
        }

        public async Task Delete(long id)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync("DELETE FROM \"orders\" WHERE id = @id;", new { id = id });
        }
    }
}