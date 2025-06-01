using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class QueryLookupRepository<TEntity> : QueryRepository<TEntity>, IQueryLookupRepository<TEntity>
    where TEntity : BaseLookupEntity
{
    protected QueryLookupRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<TEntity>> GetActiveAsync()
    {
        return await _dbSet
            .Where(l => l.IsActive == true)
            .OrderBy(l => l.ShortDescription)
            .ToListAsync();
    }
}
