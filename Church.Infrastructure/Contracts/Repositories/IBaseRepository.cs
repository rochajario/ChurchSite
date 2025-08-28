using Church.Domain.Entities;
using Church.Infrastructure.Data;
using System.Linq.Expressions;

namespace Church.Infrastructure.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity<long>
    {
        /// <summary>
        /// Get ApplicationContext
        /// </summary>
        /// <returns></returns>
        ApplicationContext GetContext();
        /// <summary>
        /// Add new entity to the repository
        /// </summary>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing entity in the repository
        /// </summary>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get an entity by its Unique ID
        /// </summary>
        /// <param name="id">Entity Unique ID</param>
        /// <returns>Entire entity</returns>
        Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an entity by its Unique ID
        /// </summary>
        /// <param name="id">Entity Unique ID</param>
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if an entity exists by its Unique ID
        /// </summary>
        /// <param name="id">Entity Unique ID</param>
        /// <returns>True or false</returns>
        Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all entities from the repository
        /// </summary>
        /// <returns>All entities located in the repository</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get paged results from the repository
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        Task<(List<TEntity>, long)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Filtered list of items
        /// </summary>
        /// <param name="filter">Filter to be used</param>
        /// <returns></returns>
        Task<List<TEntity>> GetFilteredList(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get unique item based  on filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<TEntity?> GetFiltered(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Filtered and Paged list of items
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="query">IQueriable from TEntity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<(List<TEntity>, long)> GetPagedAndFilteredAsync(int pageNumber, int pageSize, IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    }
}