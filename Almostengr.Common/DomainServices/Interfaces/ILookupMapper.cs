using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupMapper<TEntity, TResource> : IMapper<TEntity, TResource>
    where TEntity : LookupEntity<TEntity>
    where TResource : LookupResource;
