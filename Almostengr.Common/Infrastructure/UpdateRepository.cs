using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

public class UpdateRepository<TEntity> : AddRepository<TEntity>, IUpdateRepository<TEntity> where TEntity : BaseEntity
{
    protected UpdateRepository(DbContext context) : base(context) { }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
}
