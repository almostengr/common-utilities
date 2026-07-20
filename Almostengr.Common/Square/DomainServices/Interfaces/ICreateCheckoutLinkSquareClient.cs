using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface ICreateCheckoutLinkSquareClient : ISquareClient
{
    Task<Result<CreatePaymentLinkResponse>> ExecuteAsync(
       string customerId, string itemName, long amountInCents, string redirectUrl, string subscriptionPlanId = null);
}