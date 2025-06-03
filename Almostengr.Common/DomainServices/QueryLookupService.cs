using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.Shared;

namespace Almostengr.Common.DomainServices;

public class QueryLookupService<TEntity, TResource> : QueryService<TEntity, TResource>, IQueryLookupService<TEntity, TResource>
    where TEntity : BaseLookupEntity where TResource : LookupResource
{
    private readonly IQueryLookupRepository<TEntity> _lookupRepository;

    public QueryLookupService(IQueryLookupRepository<TEntity> repository) : base(repository)
    {
        _lookupRepository = repository;
    }

    public async Task<IEnumerable<LookupResource>> GetActiveAsync()
    {
        IEnumerable<TEntity> entities = await _lookupRepository.GetActiveAsync();
        return entities.Select(i => i.ToLookupResource<TEntity>()).ToList();
    }
}
