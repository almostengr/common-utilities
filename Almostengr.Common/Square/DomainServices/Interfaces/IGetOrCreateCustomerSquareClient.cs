using Almostengr.Common.Common.DomainServices.Results;
using Square;

namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface IGetOrCreateCustomerSquareClient : ISquareClient
{
    Task<Result<Customer>> ExecuteAsync(string customerId, string email, string firstName, string lastName, string phoneNumber);
}