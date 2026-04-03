using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupRepository<TEntity> : IQueryRepository<TEntity> where TEntity : LookupEntity<TEntity>
{
    Task<IEnumerable<TEntity>> GetListAsync(bool sortDescending = false, bool activeOnly = true);
}
