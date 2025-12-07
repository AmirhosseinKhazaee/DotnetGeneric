using System.Collections.Generic;
using System.Threading.Tasks;

namespace XGeneric.Repository
{
    public interface IGenericRepo<T> where T : class
    {
        // Async methods for high-concurrency WebAPI

        /// <summary>
        /// Get all entities
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get entity by key
        /// </summary>
        Task<T?> GetByIdAsync(object id);

        /// <summary>
        /// Add a new entity
        /// </summary>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// Update an existing entity
        /// </summary>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Delete entity by key
        /// </summary>
        Task<bool> DeleteAsync(object id);
    }
}
