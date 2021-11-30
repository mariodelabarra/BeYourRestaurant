using BeYourRestaurant.Platform.Core.Domain;
using BeYourRestaurant.Platform.Core.Postgres.Helpers;
using BeYourRestaurant.Platform.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.Postgres.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public abstract string _tableEntityName { get; }
        private readonly IDapperDbContext _context;

        protected BaseRepository(IDapperDbContext databaseContext)
        {
            _context = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = string.Format("SELECT * FROM {0}", _tableEntityName);

            return await _context.Connection.QueryAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(int entityId)
        {
            var query = string.Format("SELECT * FROM {0} WHERE Id = {1}", _tableEntityName, entityId);

            return await _context.Connection.QuerySingleAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(T entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            //TODO: Change for a method that returns the dictionary of parameters so I can run a store procedure instead of this
            var insertQuery = QueryHelper<T>.GenerateInsertQuery(_tableEntityName);

            return await _context.Connection.ExecuteAsync(insertQuery, entity);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int entityId)
        {
            var query = string.Format("DELETE FROM {0} WHERE Id = @Id", _tableEntityName);

            return await _context.Connection.ExecuteAsync(query, entityId);
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(T entity)
        {
            entity.LastModifiedDate = DateTime.UtcNow;
            //TODO: Change for a method that returns the dictionary of parameters so I can run a store procedure instead of this
            var updateQuery = QueryHelper<T>.GenerateUpdateQuery(_tableEntityName);

            return await _context.Connection.ExecuteAsync(updateQuery, entity);
        }
    }
}
