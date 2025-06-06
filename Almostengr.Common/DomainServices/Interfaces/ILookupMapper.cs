using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ILookupMapper<TEntity, TResource> : IMapper<TEntity, TResource>
    where TEntity : BaseLookupEntity where TResource : LookupResource;
