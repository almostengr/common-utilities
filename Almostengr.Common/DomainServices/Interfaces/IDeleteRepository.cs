using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IDeleteRepository<TEntity> : IUpdateRepository<TEntity> where TEntity : Entity
{
    void Delete(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}

