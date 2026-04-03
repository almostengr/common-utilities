using System.ComponentModel;
using System.Reflection;
using Almostengr.Common.DomainServices;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.Shared;

public static class CommonExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IAddRepository<>), typeof(AddRepository<>));
        services.AddTransient(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));
        services.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        services.AddTransient(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddTransient(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));

        services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        services.AddTransient(typeof(IQueryService<,>), typeof(QueryService<,>));
    }

    public static void AddCommonLookupServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(ILookupRepository<>), typeof(LookupRepository<>));
        services.AddTransient(typeof(ILookupMapper<,>), typeof(LookupMapper<>));
        services.AddTransient(typeof(ILookupService<,>), typeof(LookupService<,>));
    }

    public static void AddApiKeyDbServices(this IServiceCollection services)
    {
        services.AddTransient<IApiKeyRepository, ApiKeyRepository>();
        services.AddTransient<IApiKeyService, ApiKeyDbService>();
    }

    public static void AddApiKeySettingsServices(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        // todo - configuration
        services.AddTransient<IApiKeyService, ApiKeySettingsService>();
    }

    public static string ToDescription(this Enum enumValue)
    {
        FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
        {
            return enumValue.ToString();
        }
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute?.Description ?? enumValue.ToString();
    }
}
