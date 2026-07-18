using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.ApiKeys.Domain;

public class ApiKey : Entity
{
    private ApiKey()
    {
    }

    private ApiKey(int userId, string hashedKey, string createdBy) : base(Guid.Empty, createdBy)
    {
        ApplicationUserId = userId;
        KeyHash = hashedKey;
    }

    public int ApplicationUserId { get; private set; }

    [Required]
    public string KeyHash { get; private set; }

    public static Result<ApiKey> Create(int userId, string hashedKey, string createdBy)
    {
        if (userId <= 0)
        {
            return Result<ApiKey>.Failure("Invalid user.");
        }

        if (string.IsNullOrWhiteSpace(hashedKey))
        {
            return Result<ApiKey>.Failure("Key is required.");
        }

        ApiKey key = new(userId, hashedKey, createdBy);
        return Result<ApiKey>.Success(key);
    }
}