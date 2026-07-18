using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface ISubsriptionSquareClient : ISquareClient
{
    Task<Subscription> GetSubscriptionAsync(string subscriptionId);
    Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, string planVariationId, string cardId, string locationId);
    Task<Result<Subscription>> CancelSubscriptionAsync(string subscriptionId);
    Task<IEnumerable<Subscription>> GetSubscriptionsAsync(string customerId, bool activeOnly);
    Task<bool> HasSubscriptionsAsync(string customerId, bool activeOnly);
}
