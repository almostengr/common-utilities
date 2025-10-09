namespace Almostengr.Common.DomainServices.Interfaces;

public interface IApiKeyRepository
{
    Task<bool> IsValidApiKeyAsync(string apiKey);
}
