using Almostengr.Common.ApiKeys.Domain;
using Almostengr.Common.ApiKeys.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.ApiKeys.DomainServices;

public class QueryApiKeyService : ApiKeyService, IQueryApiKeyService
{
    private readonly DbSet<ApiKey> _dbSet;

    public QueryApiKeyService(
        DbContext dbContext
    )
    {
        _dbSet = dbContext.Set<ApiKey>();
    }

    public async Task<bool> Exists(string apiKey)
    {
        var exists = await _dbSet.AnyAsync(a => a.KeyHash == ToApiKeyHash(apiKey));
        return exists;
    }

    public async Task<int> GetUserIdAsync(string apiKey)
    {
        var userId = await _dbSet
            .Where(a => a.KeyHash == ToApiKeyHash(apiKey))
            .Select(a => a.ApplicationUserId)
            .SingleOrDefaultAsync();

        return userId;
    }
}
