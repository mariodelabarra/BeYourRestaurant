using Npgsql;
using System;
using System.Data;

namespace BeYourRestaurant.Platform.Core.Postgres
{
    public sealed class PostgressDbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public PostgressDbSession(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
