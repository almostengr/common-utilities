using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.Shared;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.DomainServices;

public class ApiKeyDbService : IApiKeyService
{
    private readonly ILogger<ApiKeyDbService> _logger;
    private readonly IApiKeyRepository _repository;

    public ApiKeyDbService(
        IApiKeyRepository repository,
        ILogger<ApiKeyDbService> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey)
    {
        bool isValid = await _repository.IsValidApiKeyAsync(apiKey);

        if (!isValid)
        {
            _logger.LogError(LibConstants.InvalidApiKey);
        }

        return isValid;
    }
}
