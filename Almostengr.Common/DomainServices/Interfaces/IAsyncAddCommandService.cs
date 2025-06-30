using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAsyncAddCommandService<TResource> : IAsyncCommandService<TResource> where TResource : BaseResource;
