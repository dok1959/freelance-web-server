using System;
using Npgsql;
using System.Data;

namespace FreelanceWebServer.Data.PostgreSQL
{
    public class PostgresDBContext : IDisposable
    {
        private NpgsqlConnection _connection;

        public PostgresDBContext()
        {
            _connection = new NpgsqlConnection("User ID=laqgytasafmwsd;Password=546f1f14cd1613d2efa6792d8acfc18e30fd892aa14409fb534f14360e8a325d;Host=ec2-3-230-26-112.compute-1.amazonaws.com;Port=5432;Database=d368v7dldm6c1n");
            _connection.Open();
        }

        public IDbConnection GetConnection() => _connection;

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
