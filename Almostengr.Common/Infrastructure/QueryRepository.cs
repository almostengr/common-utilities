using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected QueryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByGuidAsync(Guid guid)
    {
        return await _dbSet.Where(i => i.Guid == guid).SingleOrDefaultAsync();
    }

    public virtual async Task<bool> ExistsByGuidAsync(Guid guid)
    {
        return await _dbSet.Where(i => i.Guid == guid).AnyAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.Where(i => i.Id == id).SingleOrDefaultAsync();
    }

    public virtual async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbSet.Where(i => i.Id == id).AnyAsync();
    }
}
