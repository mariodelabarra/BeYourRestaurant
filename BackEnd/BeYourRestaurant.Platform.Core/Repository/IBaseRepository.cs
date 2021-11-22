using BeYourRestaurant.Platform.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.Repository
{
    /// <summary>
    /// Represents the common operations for the entities
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets all the data from the specified entity
        /// </summary>
        /// <returns>List of entity</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get the first match with the specified Id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Matched entity</returns>
        Task<T> GetByIdAsync(int entityId);

        /// <summary>
        /// Creates a new entity with the provide data
        /// </summary>
        /// <param name="entity">Entity to create</param>
        /// <returns>Id of the created entity</returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Find the entity with the Id and try to update him
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Id of the updated entity</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Deletes the entity with the specified Id
        /// </summary>
        /// <param name="entityId">Id of entity</param>
        /// <returns>Id of the deleted entity</returns>
        Task<int> DeleteAsync(int entityId);
    }
}
