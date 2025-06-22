using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices;

public class QueryLookupService<TEntity, TResource> : QueryService<TEntity, TResource>, IQueryLookupService<TEntity, TResource>
    where TEntity : BaseLookupEntity<TEntity> where TResource : LookupResource
{
    private readonly IQueryLookupRepository<TEntity> _lookupRepository;

    public QueryLookupService(
        IMapper<TEntity, TResource> mapper,
        IQueryLookupRepository<TEntity> repository) : base(mapper, repository)
    {
        _lookupRepository = repository;
    }

    public async Task<IEnumerable<LookupResource>> GetActiveAsync()
    {
        IEnumerable<TEntity> entities = await _lookupRepository.GetActiveAsync();
        return entities.Select(_mapper.ToResource).ToList();
    }
}
