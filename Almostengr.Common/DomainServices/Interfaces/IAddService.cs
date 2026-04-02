using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAddService<TResource> : ICommandService<TResource>
    where TResource : Resource;
