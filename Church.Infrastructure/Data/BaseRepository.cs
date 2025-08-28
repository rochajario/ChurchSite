using Church.Domain.Entities;
using Church.Infrastructure.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Church.Infrastructure.Data
{
    public class BaseRepository<TEntity>(ApplicationContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity<long>
    {
        protected DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        public virtual ApplicationContext GetContext()
        {
            return context;
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedAt = DateTime.Now;
            entity.IsActive = true;

            DbSet.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var tracked = await DbSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id), cancellationToken);
            if (entity.IsActive && tracked is not null)
            {
                context.Entry(tracked).CurrentValues.SetValues(entity);
                tracked.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .AnyAsync(e => EF.Property<long>(e, "ID") == id, cancellationToken);
        }

        public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet
                .Where(x => x.IsActive)
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

            if (entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                entity.IsActive = false;

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<(List<TEntity>, long)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var list = await DbSet
                .Where(x => x.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var size = await DbSet.Where(x => x.IsActive).LongCountAsync(cancellationToken: cancellationToken);

            return (list, size);
        }

        public virtual async Task<List<TEntity>> GetFilteredList(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .Where(filter)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetFiltered(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .FirstOrDefaultAsync(filter, cancellationToken);
        }

        public virtual async Task<(List<TEntity>, long)> GetPagedAndFilteredAsync(int pageNumber, int pageSize, IQueryable<TEntity> query, CancellationToken cancellationToken = default)
        {
            query = query.Where(x => x.IsActive);

            var list = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var size = await query.LongCountAsync(cancellationToken);

            return (list, size);
        }
    }
}
