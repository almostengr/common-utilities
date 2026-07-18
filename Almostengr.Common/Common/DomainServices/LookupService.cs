using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Resources;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Common.DomainServices;

public class LookupService<TEntity, TResource> : QueryService<TEntity, TResource>, ILookupService<TEntity, TResource>
    where TEntity : LookupEntity<TEntity>
    where TResource : LookupResource
{
    public LookupService(
        DbContext dbContext, IMapper<TEntity, TResource> mapper) : base(dbContext, mapper)
    {
    }

    public virtual async Task<IEnumerable<LookupResource>> GetListAsync(bool sortDescending = false, bool activeOnly = true)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (activeOnly)
        {
            query = query.Where(t => t.IsActive);
        }

        query = sortDescending ? query.OrderByDescending(t => t.ShortDescription) : query.OrderBy(t => t.ShortDescription);

        var entities = await query.ToListAsync();
        return entities.Select(_mapper.ToResource).ToList();
    }
}
