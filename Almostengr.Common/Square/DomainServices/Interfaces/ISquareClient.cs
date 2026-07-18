namespace Almostengr.Common.Square.DomainServices.Interfaces;

public interface ISquareClient
{
    string CreateIdempotencyKey();
}
