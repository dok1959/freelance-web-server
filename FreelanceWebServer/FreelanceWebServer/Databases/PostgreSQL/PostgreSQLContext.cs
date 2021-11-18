using System;
using Npgsql;

namespace FreelanceWebServer.Databases.PostgreSQL
{
    public class PostgreSQLContext : IDisposable
    {
        private NpgsqlConnection _connection;

        public PostgreSQLContext(string connectionString) 
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();

        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
