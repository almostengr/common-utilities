namespace Almostengr.Common.DomainServices.Interfaces;

public interface IApiKeyService
{
    Task<bool> IsValidApiKeyAsync(string apiKey);
}
