using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.ApiKeys.DomainServices;

public sealed class ApiKeyResource : Resource
{
    public string PlainTextKey { get; set; }
    public string HashedKey { get; set; }
    public string Prefix { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
}