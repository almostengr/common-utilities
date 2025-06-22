using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryLookupService<TEntity, TResource> where TEntity : BaseLookupEntity<TEntity> where TResource : LookupResource
{
    Task<IEnumerable<LookupResource>> GetActiveAsync();
}
