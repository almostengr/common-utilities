using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class LookupRepository<TEntity> : QueryRepository<TEntity>, ILookupRepository<TEntity>
    where TEntity : LookupEntity<TEntity>
{
    protected LookupRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(bool sortDescending, bool activeOnly)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (activeOnly)
        {
            query = query.Where(t => t.IsActive);
        }

        if (sortDescending)
        {
            query = query.OrderByDescending(t => t.ShortDescription);
        }

        return await query.ToListAsync();
    }
}
