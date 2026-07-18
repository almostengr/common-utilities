using System.Security.Cryptography;
using System.Text;

namespace Almostengr.Common.ApiKeys.DomainServices;

public abstract class ApiKeyService
{
    protected static string ToApiKeyHash(string plainTextKey)
    {
        if (string.IsNullOrWhiteSpace(plainTextKey))
        {
            return null;
        }

        byte[] bytes = Encoding.UTF8.GetBytes(plainTextKey);
        byte[] hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}