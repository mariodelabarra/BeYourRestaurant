using System;
using System.Data;

namespace BeYourRestaurant.Platform.Core.Postgres
{
    /// <summary>
    /// Class is helper for use and close IDbConnection
    /// </summary>
    public interface IDapperDbContext : IDisposable
    {
        /// <summary>
        /// Get opened DB Connection
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Open DB connection and Begin transaction
        /// </summary>
        IDbTransaction Transaction { get; }

        Guid Id { get; }

        /// <summary>
        /// Starts the database transaction
        /// </summary>
        void Begin();

        /// <summary>
        /// Saves the changes made on the database
        /// </summary>
        void Commit();

        /// <summary>
        /// Undo the changes made on the database
        /// </summary>
        void Rollback();
    }
}
