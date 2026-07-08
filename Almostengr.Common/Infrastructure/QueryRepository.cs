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

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(bool sortDescending = false)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (sortDescending)
        {
            query = query.OrderByDescending(t => t.Id);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByPublicIdAsync(Guid publicId)
    {
        return await _dbSet.SingleOrDefaultAsync(i => i.PublicId == publicId);
    }

    public virtual async Task<bool> ExistsByPublicIdAsync(Guid publicId)
    {
        return await _dbSet.AnyAsync(i => i.PublicId == publicId);
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.SingleOrDefaultAsync(i => i.Id == id);
    }

    public virtual async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbSet.AnyAsync(i => i.Id == id);
    }
}
