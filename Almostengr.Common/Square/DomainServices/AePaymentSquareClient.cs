using Square;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;

namespace Almostengr.Common.Square.DomainServices;

public class AePaymentSquareClient : AeSquareClient, IPaymentSquareClient
{
    public AePaymentSquareClient(
        ILogger<AeSquareClient> logger, IOptions<SquareSettings> options) : base(logger, options)
    {
    }

    public Task<Payment> ChargeOneTimeAsync(string customerId, long amountInCents, string sourceId, string idempotencyKey)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateCheckoutUrlAsync(string customerId, string planVariationId, string redirectUrl)
    {
        throw new NotImplementedException();
    }
}
