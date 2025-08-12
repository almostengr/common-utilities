using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class LookupRepository<TEntity> : QueryRepository<TEntity>, ILookupRepository<TEntity>
    where TEntity : BaseLookupEntity<TEntity>
{
    protected LookupRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(bool activeOnly = true)
    {
        if (activeOnly)
        {
            return await _dbSet
                .Where(l => l.IsActive == true)
                .OrderBy(l => l.ShortDescription)
                .ToListAsync();
        }

        return await _dbSet
            .OrderBy(l => l.ShortDescription)
            .ToListAsync();
    }
}
