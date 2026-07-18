using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface IAddService<TResource> : ICommandService<TResource>
    where TResource : Resource;
