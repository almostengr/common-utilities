using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface ICustomerSquareClient : ISquareClient
{
    Task<Result<Customer>> GetOrCreateCustomerAsync(string customerId, string email, string firstName, string lastName, string phoneNumber);
}