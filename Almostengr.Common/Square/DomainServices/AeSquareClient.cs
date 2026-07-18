using Square;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ISquareClient = Almostengr.Common.Square.DomainServices.Interfaces.ISquareClient;
using Almostengr.Common.Square.Shared;

namespace Almostengr.Common.Square.DomainServices;

public class AeSquareClient : ISquareClient
{
    protected readonly SquareSettings _appSettings;
    protected readonly ILogger<AeSquareClient> _logger;
    protected readonly SquareClient _client;

    public AeSquareClient(
        ILogger<AeSquareClient> logger,
        IOptions<SquareSettings> options
    )
    {
        _appSettings = options.Value;
        _logger = logger;

        _client = new SquareClient(
            _appSettings.Token,
            new ClientOptions
            {
                MaxRetries = _appSettings.MaxRetries,
                BaseUrl = _appSettings.IsProduction ? SquareEnvironment.Production : SquareEnvironment.Sandbox
            }
        );
    }

    public string CreateIdempotencyKey()
    {
        return Guid.NewGuid().ToString();
    }

    public ICustomersClient Customers => _client.Customers;
    public ILocationsClient Locations => _client.Locations;
    public IOrdersClient Orders => _client.Orders;
    public IPaymentsClient Payments => _client.Payments;
    public ISubscriptionsClient Subscriptions => _client.Subscriptions;
}
