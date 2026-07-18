namespace Almostengr.Common.Square.Shared;

public class SquareSettings
{
    public bool IsProduction { get; init; } = false;
    public string Token { get; init; } = string.Empty;
    public int MaxRetries { get; init; } = 3;
    public string LocationId { get; init; }  = string.Empty;
}
