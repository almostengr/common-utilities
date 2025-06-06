using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IMapper<TEntity, TResource> where TEntity : BaseEntity where TResource : BaseResource
{
    TResource ToResource(TEntity entity);
}
