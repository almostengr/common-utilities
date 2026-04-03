using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IDeleteService<TResource> : ICommandService<TResource>
    where TResource : Resource;
