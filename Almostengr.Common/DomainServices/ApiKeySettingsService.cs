using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Almostengr.Common.DomainServices;

public class ApiKeySettingsService : IApiKeyService
{
    private readonly ApiKeySettings _settings;
    private readonly ILogger<ApiKeySettingsService> _logger;

    public ApiKeySettingsService(IOptions<ApiKeySettings> options,
    ILogger<ApiKeySettingsService> logger)
    {
        _logger = logger;
        _settings = options.Value;
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey)
    {
        bool isValid = _settings.Keys.Contains(apiKey);
        if (!isValid)
        {
            _logger.LogError(LibConstants.InvalidApiKey);
        }

        return await Task.Run(() => isValid);
    }
}
