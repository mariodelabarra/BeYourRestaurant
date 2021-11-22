using Npgsql;
using System;
using System.Data;

namespace BeYourRestaurant.Platform.Core.Postgres
{
    public class PostgresSession : IDisposable
    {
        private IDbConnection _connection;
        DapperDbContext _dbContext = null;

        public PostgresSession(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _dbContext = new DapperDbContext(_connection);
        }

        public DapperDbContext DataBaseContext
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection.Dispose();
        }
    }
}
