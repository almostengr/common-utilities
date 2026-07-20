using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Square;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;
using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.Square.DomainServices;

public class QuerySubscriptionSquareClient : AeSquareClient, IQuerySubscriptionSquareClient
{
    public QuerySubscriptionSquareClient(
        ILogger<AeSquareClient> logger,
        IOptions<SquareSettings> options) : base(logger, options)
    {
    }

    public async Task<Result<GetSubscriptionResponse>> GetSubscriptionAsync(string subscriptionId)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

            var subscriptionRequest = new GetSubscriptionsRequest()
            {
                SubscriptionId = subscriptionId
            };

            var subscriptionResponse = await Subscriptions.GetAsync(subscriptionRequest);
            return Result<GetSubscriptionResponse>.Success(subscriptionResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<GetSubscriptionResponse>.Failure(ex.Message);
        }
    }

    public async Task<Result<SearchSubscriptionsResponse>> SearchSubscriptionsAsync(string customerId)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(customerId, nameof(customerId));

            var searchSubscriptionsRequest = new SearchSubscriptionsRequest()
            {
                Query = new SearchSubscriptionsQuery()
                {
                    Filter = new SearchSubscriptionsFilter()
                    {
                        CustomerIds = [customerId],
                        LocationIds = [_appSettings.LocationId],
                    },
                }
            };

            var searchResponse = await Subscriptions.SearchAsync(searchSubscriptionsRequest);

            Result<SearchSubscriptionsResponse> result = Result<SearchSubscriptionsResponse>.Create();
            result.SetValue(searchResponse);

            if (searchResponse.Errors.Any())
            {
                result.AddError(searchResponse.Errors.ToErrorString());
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<SearchSubscriptionsResponse>.Failure(ex.Message);
        }
    }

    public async Task<bool> HasSubscriptionsAsync(string customerId)
    {
        var result = await SearchSubscriptionsAsync(customerId);
        return result.Succeeded ? result.Value.Subscriptions.Count() > 0 : false;
    }
}
