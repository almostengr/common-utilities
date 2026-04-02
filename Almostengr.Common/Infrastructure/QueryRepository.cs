using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : Entity
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

    public virtual async Task<TEntity> GetByPublicIdAsync(Guid publicId)
    {
        return await _dbSet.Where(i => i.PublicId == publicId).SingleOrDefaultAsync();
    }

    public virtual async Task<bool> ExistsByPublicIdAsync(Guid publicId)
    {
        return await _dbSet.Where(i => i.PublicId == publicId).AnyAsync();
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
