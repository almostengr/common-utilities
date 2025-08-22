using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupService<TEntity, TResource> where TEntity : BaseLookupEntity<TEntity> where TResource : LookupResource
{
    Task<IEnumerable<LookupResource>> GetListAsync(bool activeOnly = true);
}
