using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAsyncDeleteCommandService<TResource> : IAsyncCommandService<TResource> where TResource : BaseResource;