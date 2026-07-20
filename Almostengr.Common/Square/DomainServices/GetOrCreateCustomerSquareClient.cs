using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Square;

namespace Almostengr.Common.Square.DomainServices;

public class GetOrCreateCustomerSquareClient : AeSquareClient, IGetOrCreateCustomerSquareClient
{
    public GetOrCreateCustomerSquareClient(
        ILogger<AeSquareClient> logger, 
        IOptions<SquareSettings> options) : base(logger, options)
    {
    }

    public async Task<Result<Customer>> ExecuteAsync(
        string customerId, string email, string firstName, string lastName, string phoneNumber)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                var customerResponse = await Customers.GetAsync(
                    new GetCustomersRequest()
                    {
                        CustomerId = customerId
                    });
                if (customerResponse.Customer != null)
                {
                    return Result<Customer>.Success(customerResponse.Customer);
                }
            }

            email = email.ToLower().Trim();
            if (!string.IsNullOrWhiteSpace(email))
            {
                var searchCustomer = new SearchCustomersRequest
                {
                    Query = new CustomerQuery
                    {
                        Filter = new CustomerFilter
                        {
                            EmailAddress = new CustomerTextFilter
                            {
                                Exact = email
                            }
                        }
                    }
                };

                var searchResponse = await Customers.SearchAsync(searchCustomer);
                if (searchResponse.Customers != null && searchResponse.Customers.Any())
                {
                    return Result<Customer>.Success(searchResponse.Customers.First());
                }
            }

            phoneNumber = phoneNumber.Trim();
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                var searchCustomer = new SearchCustomersRequest
                {
                    Query = new CustomerQuery
                    {
                        Filter = new CustomerFilter
                        {
                            PhoneNumber = new CustomerTextFilter
                            {
                                Exact = phoneNumber
                            }
                        }
                    }
                };

                var searchResponse = await Customers.SearchAsync(searchCustomer);
                if (searchResponse.Customers != null && searchResponse.Customers.Any())
                {
                    return Result<Customer>.Success(searchResponse.Customers.First());
                }
            }

            lastName = lastName.Trim();
            firstName = firstName.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lastName))
            {
                return Result<Customer>.Failure("Email Address and Last Name are required.");
            }

            var createCustomer = new CreateCustomerRequest()
            {
                FamilyName = lastName,
                GivenName = firstName,
                EmailAddress = email,
                PhoneNumber = phoneNumber,
            };

            var createResponse = await Customers.CreateAsync(createCustomer);
            if (createResponse.Errors.Any())
            {
                return Result<Customer>.Failure(string.Join(" ", createResponse.Errors));
            }

            return Result<Customer>.Success(createResponse.Customer);
        }
        catch (SquareApiException sqEx)
        {
            var errorMessages = string.Join(" | ", sqEx.Errors.Select(e => e.Detail));
            _logger.LogError(errorMessages);
            return Result<Customer>.Failure(errorMessages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result<Customer>.Failure(ex.Message);
        }
    }
}