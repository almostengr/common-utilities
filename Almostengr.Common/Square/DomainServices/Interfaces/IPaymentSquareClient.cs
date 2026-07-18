using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface IPaymentSquareClient: ISquareClient
{
    Task<Payment> ChargeOneTimeAsync(string customerId, long amountInCents, string sourceId, string idempotencyKey);
    Task<string> GenerateCheckoutUrlAsync(string customerId, string planVariationId, string redirectUrl);
}