using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Square;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;

namespace Almostengr.Common.Square.DomainServices;

public class AeSubscriptionSquareClient : AeSquareClient, ISubsriptionSquareClient
{
    public AeSubscriptionSquareClient(
        ILogger<AeSquareClient> logger, 
        IOptions<SquareSettings> options) : base(logger, options)
    {
    }

    public async Task<Subscription> GetSubscriptionAsync(string subscriptionId)
    {
        var subscriptionRequest = new GetSubscriptionsRequest()
        {
            SubscriptionId = subscriptionId
        };

        var subscriptionResponse = await Subscriptions.GetAsync(subscriptionRequest);
        return subscriptionResponse.Subscription;
    }

    public async Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, string planVariationId, string cardId, string locationId)
    {
        try
        {
            var createRequest = new CreateSubscriptionRequest()
            {
                CustomerId = customerId,
                PlanVariationId = planVariationId,
                CardId = cardId,
                LocationId = locationId
            };

            var subscriptionResponse = await Subscriptions.CreateAsync(createRequest);
            if (subscriptionResponse.Errors.Any())
            {
                return Result<Subscription>.Failure(string.Join(" ", subscriptionResponse.Errors));
            }

            return Result<Subscription>.Success(subscriptionResponse.Subscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result<Subscription>.Failure(ex.Message);
        }
    }

    public async Task<Result<Subscription>> CancelSubscriptionAsync(string subscriptionId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                return Result<Subscription>.Failure("Subscription not found.");
            }

            var subscription = await GetSubscriptionAsync(subscriptionId);
            if (subscription == null)
            {
                return Result<Subscription>.Failure("Subscription not found.");
            }

            if (subscription.Status == SubscriptionStatus.Canceled)
            {
                return Result<Subscription>.Failure("Subscription is not active.");
            }

            var cancelRequest = new CancelSubscriptionsRequest()
            {
                SubscriptionId = subscriptionId
            };

            var subscriptionResponse = await Subscriptions.CancelAsync(cancelRequest);
            if (subscriptionResponse.Errors.Any())
            {
                return Result<Subscription>.Failure(string.Join(" ", subscriptionResponse.Errors));
            }

            return Result<Subscription>.Success(subscriptionResponse.Subscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return Result<Subscription>.Failure(ex.Message);
        }
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsAsync(string customerId, bool activeOnly)
    {
        if (string.IsNullOrWhiteSpace(customerId))
        {
            return Enumerable.Empty<Subscription>();
        }

        var searchSubscriptionsRequest = new SearchSubscriptionsRequest()
        {
            Query = new SearchSubscriptionsQuery()
            {
                Filter = new SearchSubscriptionsFilter()
                {
                    CustomerIds = [customerId],
                },
            }
        };

        var searchResponse = await Subscriptions.SearchAsync(searchSubscriptionsRequest);

        if (activeOnly)
        {
            return searchResponse.Subscriptions.Where(s => s.Status == SubscriptionStatus.Active);
        }

        return searchResponse.Subscriptions;
    }

    public async Task<bool> HasSubscriptionsAsync(string customerId, bool activeOnly)
    {
        var subscriptions = await GetSubscriptionsAsync(customerId, activeOnly);
        return subscriptions.Count() > 0;
    }
}
