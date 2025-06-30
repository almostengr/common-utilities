using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.DomainServices.Interfaces;

[Obsolete]
public interface ICommandService<TResource> where TResource : BaseResource
{
    Task<Result<TResource>> ExecuteAsync(TResource resource, bool commitTransaction = true);
}
