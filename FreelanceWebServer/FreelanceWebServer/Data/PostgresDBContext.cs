using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FreelanceWebServer.Data.PostgreSQL
{
    public class PostgresDBContext : IDisposable
    {
        private const int ConnectionsCount = 20;
        private readonly NpgsqlConnection[] _connectionsPool;
        private readonly string _connectionString;

        public PostgresDBContext(IConfiguration configuration)
        {
            _connectionsPool = new NpgsqlConnection[ConnectionsCount];
            _connectionString = configuration.GetConnectionString("PostgresHerokuConnection");
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IDbConnection> GetFreeConnection()
        {
            for(int i = 0; i < ConnectionsCount; i++)
            {
                if(_connectionsPool[i] == null)
                {
                    _connectionsPool[i] = new NpgsqlConnection(_connectionString);
                    await _connectionsPool[i].OpenAsync();

                    return _connectionsPool[i];
                }

                if (_connectionsPool[i].FullState == ConnectionState.Open)
                    return _connectionsPool[i];
            }

            return _connectionsPool[0];
        }

        public void Dispose()
        {
            foreach (var connection in _connectionsPool)
                connection.Close();
        }
    }
}
