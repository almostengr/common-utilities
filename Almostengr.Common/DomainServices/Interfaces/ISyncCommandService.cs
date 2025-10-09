using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface ISyncCommandService<TResource> where TResource : BaseResource
{
    Result<TResource> Execute(TResource resource, bool commitTransaction = true);
}
