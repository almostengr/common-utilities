using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAsyncUpdateCommandService<TResource> : IAsyncCommandService<TResource> where TResource : BaseResource;
