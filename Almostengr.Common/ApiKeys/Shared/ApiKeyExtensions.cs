using Almostengr.Common.ApiKeys.DomainServices;
using Almostengr.Common.ApiKeys.DomainServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.Common.ApiKeys.Shared;

public static class ApiKeyExtensions
{
    public static void AddApiKeyServices(this IServiceCollection services)
    {
        services.AddTransient<IQueryApiKeyService, QueryApiKeyService>();
        services.AddTransient<IUpsertApiKeyService, UpsertApiKeyService>();
    }
}