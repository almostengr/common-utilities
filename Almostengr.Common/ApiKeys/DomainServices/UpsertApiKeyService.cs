using System.Security.Cryptography;
using Almostengr.Common.ApiKeys.Domain;
using Almostengr.Common.ApiKeys.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.ApiKeys.DomainServices;

public sealed class UpsertApiKeyService : ApiKeyService, IUpsertApiKeyService
{
    private readonly DbContext _dbContext;
    private readonly DbSet<ApiKey> _dbSet;
    private readonly ILogger<UpsertApiKeyService> _logger;

    public UpsertApiKeyService(
        DbContext dbContext,
        ILogger<UpsertApiKeyService> logger)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<ApiKey>();
        _logger = logger;
    }

    public async Task<Result<ApiKeyResource>> ExecuteAsync(ApiKeyResource resource, bool commitTransaction = true)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            var entity = await _dbSet.SingleOrDefaultAsync(r => r.ApplicationUserId == resource.UserId);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }

            var newResource = GenerateApiKey();

            var result = ApiKey.Create(resource.UserId, newResource.HashedKey, resource.ModifiedBy);
            if (result.Failed)
            {
                return Result<ApiKeyResource>.Failure(result.Errors);
            }

            await _dbSet.AddAsync(result.Value);

            if (commitTransaction)
            {
                await _dbContext.SaveChangesAsync();
            }

            return Result<ApiKeyResource>.Success(newResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<ApiKeyResource>.Failure(ex.Message);
        }
    }

    private static ApiKeyResource GenerateApiKey()
    {
        byte[] randomBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        string base64Key = Convert.ToBase64String(randomBytes)
            .Replace("/", "_")
            .Replace("+", "-")
            .Replace("=", "");

        string plainTextKey = base64Key;
        string hashedKey = ToApiKeyHash(plainTextKey);
        string keyPrefix = plainTextKey.Substring(0, 9); // e.g., "city_ABCD"

        return new ApiKeyResource
        {
            PlainTextKey = plainTextKey,
            HashedKey = hashedKey,
            Prefix = keyPrefix
        };
    }
}
