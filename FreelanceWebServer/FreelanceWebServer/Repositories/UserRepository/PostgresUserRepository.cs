using System.Threading.Tasks;
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

        public async Task Add(User user)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync(
                "INSERT INTO \"users\" (username, surname, name, phone_number, hashed_password, role_id) " +
                "VALUES (@Username, @Surname, @Name, @PhoneNumber, @HashedPassword, @RoleId);", user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryAsync<User>("SELECT * FROM \"users\";");
        }

        public async Task<User> Get(long id)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"users\" WHERE id = @id;", new { id = id });
        }

        public async Task<User> GetByUsername(string username)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"users\" WHERE username = @username;", new { username = username });
        }

        public async Task<User> GetByPhoneNumber(string phoneNumber)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"users\" WHERE phone_number = @phoneNumber;", new { phoneNumber = phoneNumber });
        }

        public async Task<User> GetByRefreshToken(string token)
        {
            var connection = await _context.GetFreeConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"users\" WHERE refresh_token = @token;", new { token = token });
        }

        public async Task Update(User user)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync(
                "UPDATE * FROM \"users\" " +
                "SET username = @Username, surname = @Surname, name = @Name, phone_number = @PhoneNumber " +
                "WHERE id = @Id;", user);
        }

        public async Task UpdateRefreshToken(long id, string token)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync(
                "UPDATE \"users\" SET refresh_token = @token " +
                "WHERE id = @id;", new { id = id, token = token });
        }

        public async Task Delete(long id)
        {
            var connection = await _context.GetFreeConnection();
            await connection.ExecuteAsync("DELETE FROM \"users\" WHERE id = @id;", new { id = id });
        }
    }
}
