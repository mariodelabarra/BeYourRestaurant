using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;

namespace BeYourRestaurant.Platform.Core.Postgres
{
    /// <inheritdoc/>
    public class DapperDbContext : IDapperDbContext
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private Guid _id = Guid.Empty;

        internal DapperDbContext(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }

        IDbConnection IDapperDbContext.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IDapperDbContext.Transaction
        {
            get { return _transaction; }
        }
        Guid IDapperDbContext.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
