using System.ComponentModel;
using System.Reflection;
using Almostengr.Common.Common.DomainServices;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.Common.Shared;

public static class CommonExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        services.AddTransient(typeof(IQueryService<,>), typeof(QueryService<,>));
    }

    public static void AddMvcServices(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }

    public static void AddCommonLookupServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(ILookupMapper<,>), typeof(LookupMapper<>));
        services.AddTransient(typeof(ILookupService<,>), typeof(LookupService<,>));
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
