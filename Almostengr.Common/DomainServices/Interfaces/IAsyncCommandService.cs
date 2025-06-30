using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAsyncCommandService<TResource> where TResource : BaseResource
{
    Task<Result<TResource>> ExecuteAsync(TResource resource);
}
