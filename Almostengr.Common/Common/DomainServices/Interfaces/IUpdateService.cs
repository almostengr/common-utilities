using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface IUpdateService<TResource> : ICommandService<TResource>
    where TResource : Resource;
