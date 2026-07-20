using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface ICancelSubscriptionSquareClient : ISquareClient
{
    Task<Result<CancelSubscriptionResponse>> ExecuteAsync(string subscriptionId);
}
