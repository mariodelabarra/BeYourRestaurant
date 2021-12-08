using BeYourRestaurant.Platform.Core.Domain;
using BeYourRestaurant.Platform.Core.Postgres.Helpers;
using BeYourRestaurant.Platform.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.Postgres.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly string _tableEntityName;
        private readonly PostgressDbSession _session;

        protected BaseRepository(PostgressDbSession session, string tableEntityName)
        {
            _session = session;
            _tableEntityName = string.Format(@"""{0}""", tableEntityName);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = string.Format(@"SELECT * FROM ""{0}""", _tableEntityName);

            return await _session.Connection.QueryAsync<T>(query, null, _session.Transaction);
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(int entityId)
        {
            var query = string.Format(@"SELECT * FROM {0} WHERE ""Id"" = {1}", _tableEntityName, entityId);

            return await _session.Connection.QuerySingleAsync<T>(query, null, _session.Transaction);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(T entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            //TODO: Change for a method that returns the dictionary of parameters so I can run a store procedure instead of this
            var insertQuery = QueryHelper<T>.GenerateInsertQuery(_tableEntityName);

            return await _session.Connection.ExecuteAsync(insertQuery, entity, _session.Transaction);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int entityId)
        {
            var query = string.Format(@"DELETE FROM {0} WHERE ""Id"" = @Id", _tableEntityName);

            return await _session.Connection.ExecuteAsync(query, entityId, _session.Transaction);
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(T entity)
        {
            entity.LastModifiedDate = DateTime.UtcNow;
            //TODO: Change for a method that returns the dictionary of parameters so I can run a store procedure instead of this
            var updateQuery = QueryHelper<T>.GenerateUpdateQuery(_tableEntityName);

            return await _session.Connection.ExecuteAsync(updateQuery, entity, _session.Transaction);
        }
    }
}
