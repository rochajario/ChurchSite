using Church.Domain.Entities;
using Church.Infrastructure.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Church.Infrastructure.Data
{
    public class UserDataRepository(ApplicationContext context) : BaseRepository<UserData>(context), IUserDataRepository
    {
        public override async Task<UserData?> GetFiltered(Expression<Func<UserData, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.IsActive)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(filter, cancellationToken);
        }
    }
}
