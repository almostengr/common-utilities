using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

[Obsolete]
public interface IAddService<TEntity, TResource> : ICommandService<TResource>
    where TResource : BaseResource
    where TEntity : BaseEntity;
