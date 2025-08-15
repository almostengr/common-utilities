using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseLookupEntity<TEntity>
{
    Task<IEnumerable<TEntity>> GetListAsync(bool activeOnly = true);
}
