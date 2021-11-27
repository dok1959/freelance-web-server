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
            await connection.ExecuteAsync("INSERT INTO \"orders\" (title, customer_id) VALUES (@Title, @CustomerId);", order);
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
            await connection.ExecuteAsync(
                "UPDATE \"orders\" SET title = @Title, employee_id = @EmployeeId " +
                "WHERE id = @Id;", order);
        }

        public async Task Delete(long id)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync("DELETE FROM \"orders\" WHERE id = @id;", new { id = id });
        }
    }
}