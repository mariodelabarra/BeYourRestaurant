using BeYourRestaurant.Platform.Core.Domain;
using BeYourRestaurant.Platform.Core.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.Postgres.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected IDbConnection _connection;

        protected BaseRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        /// <inheritdoc/>
        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
