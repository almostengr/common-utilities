namespace Almostengr.Common.Email.Shared;

public class EmailSettings
{
    public string Hostname { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public int PortNumber { get; init; } = 557;
    public bool UseSsl { get; init; } = true; 
}
