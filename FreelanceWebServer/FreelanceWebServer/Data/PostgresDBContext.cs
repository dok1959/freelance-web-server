using System;
using Npgsql;

namespace FreelanceWebServer.Data.PostgreSQL
{
    public class PostgresDBContext : IDisposable
    {
        private NpgsqlConnection _connection;

        public PostgresDBContext(string connectionString) 
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();

        }

        public NpgsqlDataReader ExecuteReader(string command)
        {
            NpgsqlCommand sqlCommand = new NpgsqlCommand(command, _connection);
            return sqlCommand.ExecuteReader();
        }

        public int ExecuteNonQuery(string command)
        {
            NpgsqlCommand sqlCommand = new NpgsqlCommand(command, _connection);
            return sqlCommand.ExecuteNonQuery();
        }


        public void Dispose()
        {
            _connection.Close();
        }
    }
}
