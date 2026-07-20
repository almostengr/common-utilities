using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface IQuerySubscriptionSquareClient : ISquareClient
{
    Task<Result<GetSubscriptionResponse>> GetSubscriptionAsync(string subscriptionId);
    Task<Result<SearchSubscriptionsResponse>> SearchSubscriptionsAsync(string customerId);
    Task<bool> HasSubscriptionsAsync(string customerId);
}