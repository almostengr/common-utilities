using Almostengr.Common.Square.DomainServices;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.Common.Square.Shared;

public static class SquareExtensions
{
    public static void AddAeSquareServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICustomerSquareClient, AeCustomerSquareClient>();
        services.AddTransient<IPaymentSquareClient, AePaymentSquareClient>();
        services.AddTransient<ISquareClient, AeSquareClient>();
        services.AddTransient<ISubsriptionSquareClient, AeSubscriptionSquareClient>();

        services.Configure<SquareSettings>(configuration.GetSection(nameof(SquareSettings)));
    }
}