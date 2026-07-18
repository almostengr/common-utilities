using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Resources;
using Almostengr.Common.Common.DomainServices.Results;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.Common.DomainServices;

public abstract class CommandService<TResource, TService> : ICommandService<TResource>
    where TService : class
    where TResource : Resource
{
    protected readonly ILogger<TService> _logger;

    protected CommandService(ILogger<TService> logger)
    {
        _logger = logger;
    }

    public abstract Task<Result<TResource>> ExecuteAsync(TResource resource, bool commitTransaction = true);
}
