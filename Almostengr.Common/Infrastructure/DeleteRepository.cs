using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class DeleteRepository<TEntity> : UpdateRepository<TEntity>, IDeleteRepository<TEntity> where TEntity : BaseEntity
{
    protected DeleteRepository(DbContext context) : base(context) { }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
