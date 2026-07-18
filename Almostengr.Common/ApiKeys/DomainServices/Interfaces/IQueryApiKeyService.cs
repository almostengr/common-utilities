namespace Almostengr.Common.ApiKeys.DomainServices.Interfaces;

public interface IQueryApiKeyService
{
    Task<bool> Exists(string apiKey);
    Task<int> GetUserIdAsync(string apiKey);
}