using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupService<TEntity, TResource> where TEntity : LookupEntity<TEntity> where TResource : LookupResource
{
    Task<IEnumerable<LookupResource>> GetListAsync(bool sortDescending = false, bool activeOnly = true);
}
