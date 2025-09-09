using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Common.Infrastructure;

internal sealed class ApiKeyRepository : UpdateRepository<ApiKey>, IApiKeyRepository
{
    public ApiKeyRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey)
    {
        return await _dbSet.Where(a => a.Key == apiKey && a.IsActive == true).AnyAsync();
    }
}