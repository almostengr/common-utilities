using Square;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;
using Square.Checkout_;
using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.Square.DomainServices;

public abstract class CreateCheckoutLinkSquareClient : AeSquareClient, ICreateCheckoutLinkSquareClient
{
    public CreateCheckoutLinkSquareClient(
        ILogger<AeSquareClient> logger,
        IOptions<SquareSettings> options
        ) : base(logger, options)
    {
    }

    public async Task<Result<CreatePaymentLinkResponse>> ExecuteAsync(
        string customerId, string itemName, long amountInCents, string redirectUrl, string planVariationId = null)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(customerId, nameof(customerId));
            ArgumentException.ThrowIfNullOrWhiteSpace(itemName, nameof(itemName));

            if (amountInCents <= 0)
            {
                throw new ArgumentException("Invalid amount.");
            }

            CreatePaymentLinkResponse linkResponse = await Checkout.PaymentLinks.CreateAsync(
                new CreatePaymentLinkRequest
                {
                    IdempotencyKey = CreateIdempotencyKey(),
                    QuickPay = new QuickPay
                    {
                        LocationId = _appSettings.LocationId,
                        Name = itemName,
                        PriceMoney = new Money
                        {
                            Amount = amountInCents,
                            Currency = Currency.Usd,
                        },
                    },
                    CheckoutOptions = new CheckoutOptions
                    {
                        SubscriptionPlanId = planVariationId,
                        EnableCoupon = false,
                        RedirectUrl = redirectUrl,
                        EnableLoyalty = false,
                    }
                });

            Result<CreatePaymentLinkResponse> result = Result<CreatePaymentLinkResponse>.Create();
            result.SetValue(linkResponse);

            if (linkResponse.Errors.Any())
            {
                result.AddError(linkResponse.Errors.ToErrorString());
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<CreatePaymentLinkResponse>.Failure(ex.Message);
        }
    }
}
