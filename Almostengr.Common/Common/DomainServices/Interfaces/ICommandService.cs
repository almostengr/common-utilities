using Almostengr.Common.Common.DomainServices.Resources;
using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface ICommandService<TResource> where TResource : Resource
{
    Task<Result<TResource>> ExecuteAsync(TResource resource, bool commitTransaction = true);
}
