using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryLookupRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseLookupEntity<TEntity>
{
    Task<IEnumerable<TEntity>> GetActiveAsync();
}