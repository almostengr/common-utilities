using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface ILookupMapper<TEntity, TResource> : IMapper<TEntity, TResource>
    where TEntity : LookupEntity<TEntity>
    where TResource : LookupResource
{
    KeyValuePair<int, string> ToKeyValuePair(TEntity entity);
}
