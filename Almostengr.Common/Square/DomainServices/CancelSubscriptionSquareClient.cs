using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Square;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;

namespace Almostengr.Common.Square.DomainServices;

public class CancelSubscriptionSquareClient : AeSquareClient, ICancelSubscriptionSquareClient
{
    private readonly IQuerySubscriptionSquareClient _querySubscription;

    public CancelSubscriptionSquareClient(
        ILogger<AeSquareClient> logger,
        IOptions<SquareSettings> options,
        IQuerySubscriptionSquareClient querySubscripton
        ) : base(logger, options)
    {
        _querySubscription = querySubscripton;
    }

    public async Task<Result<CancelSubscriptionResponse>> ExecuteAsync(string subscriptionId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                return Result<CancelSubscriptionResponse>.Failure("Subscription not found.");
            }

            var subscription = await _querySubscription.GetSubscriptionAsync(subscriptionId);
            if (subscription.Value == null)
            {
                return Result<CancelSubscriptionResponse>.Failure("Subscription not found.");
            }

            if (subscription.Value.Subscription.Status == SubscriptionStatus.Canceled)
            {
                return Result<CancelSubscriptionResponse>.Failure("Subscription is not active.");
            }

            var cancelRequest = new CancelSubscriptionsRequest()
            {
                SubscriptionId = subscriptionId
            };

            var subscriptionResponse = await Subscriptions.CancelAsync(cancelRequest);

            Result<CancelSubscriptionResponse> result = Result<CancelSubscriptionResponse>.Create();
            result.SetValue(subscriptionResponse);

            if (subscriptionResponse.Errors.Any())
            {
                result.AddError(subscriptionResponse.Errors.ToErrorString());
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result<CancelSubscriptionResponse>.Failure(ex.Message);
        }
    }
}
