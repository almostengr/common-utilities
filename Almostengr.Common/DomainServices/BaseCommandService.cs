using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.DomainServices.Results;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.DomainServices;

public abstract class BaseAyncCommandService<TResource, TService> : IAsyncCommandService<TResource>
    where TService : class
    where TResource : BaseResource
{
    protected readonly ILogger<TService> _logger;

    protected BaseAyncCommandService(ILogger<TService> logger)
    {
        _logger = logger;
    }

    public abstract Task<Result<TResource>> ExecuteAsync(TResource resource);
}
