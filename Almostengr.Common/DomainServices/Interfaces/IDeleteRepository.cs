using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IDeleteRepository<TEntity> : IUpdateRepository<TEntity> where TEntity : Entity
{
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}

