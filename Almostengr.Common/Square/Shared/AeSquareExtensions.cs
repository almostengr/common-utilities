using Almostengr.Common.Square.DomainServices;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Square;

namespace Almostengr.Common.Square.Shared;

public static class SquareExtensions
{
    public static void AddAeSquareServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<DomainServices.Interfaces.ISquareClient, AeSquareClient>();
        services.AddTransient<ICancelSubscriptionSquareClient, CancelSubscriptionSquareClient>();
        services.AddTransient<ICreateCheckoutLinkSquareClient, CreateCheckoutLinkSquareClient>();
        services.AddTransient<IGetOrCreateCustomerSquareClient, GetOrCreateCustomerSquareClient>();
        services.AddTransient<IQuerySubscriptionSquareClient, QuerySubscriptionSquareClient>();

        services.Configure<SquareSettings>(configuration.GetSection(nameof(SquareSettings)));
    }

    public static string ToErrorString(this IEnumerable<Error> errors)
    {
        return string.Join(" ", errors);
    }
}