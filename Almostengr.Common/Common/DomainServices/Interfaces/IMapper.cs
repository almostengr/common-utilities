using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface IMapper<TEntity, TResource> where TEntity : Entity where TResource : Resource
{
    TResource ToResource(TEntity entity);
}
