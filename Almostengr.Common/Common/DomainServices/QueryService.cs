using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Resources;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Common.DomainServices;

public class QueryService<TEntity, TResource> : IQueryService<TEntity, TResource>
    where TEntity : Entity 
    where TResource : Resource
{
    protected readonly IMapper<TEntity, TResource> _mapper;
    protected readonly DbSet<TEntity> _dbSet;

    public QueryService(
        DbContext dbContext,
        IMapper<TEntity, TResource> mapper)
    {
        _mapper = mapper;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TResource>> GetListAsync(bool sortDescending = false)
    {
        IEnumerable<TEntity> entities = await GetEntityListAsync(sortDescending);
        return entities.Select(_mapper.ToResource).ToList();
    }

    public virtual async Task<bool> ExistsByPublicIdAsync(Guid publicId)
    {
        var exists = await _dbSet.AnyAsync(i => i.PublicId == publicId);
        return exists;
    }

    public virtual async Task<TResource> GetByPublicIdAsync(Guid publicId)
    {
        TEntity entity = await GetEntityByPublicIdAsync(publicId);
        return _mapper.ToResource(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetEntityListAsync(bool sortDescending = false)
    {
        var query = _dbSet.AsQueryable();

        query = sortDescending ? query.OrderByDescending(i => i.Id) : query.OrderBy(i => i.Id);

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetEntityByPublicIdAsync(Guid publicId)
    {
        TEntity entity = await _dbSet.SingleOrDefaultAsync(i => i.PublicId == publicId);
        return entity;
    }

    public virtual async Task<bool> ExistsByIdAsync(int id)
    {
        bool exist = await _dbSet.AnyAsync(i => i.Id == id);
        return exist;
    }

    public virtual async Task<TEntity> GetEntityByIdAsync(int id)
    {
        var entity = await _dbSet.SingleOrDefaultAsync(i => i.Id == id);
        return entity;
    }
}
