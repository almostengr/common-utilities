using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

[Obsolete]
public interface IUpdateService<TResource> : ICommandService<TResource> where TResource : BaseResource;