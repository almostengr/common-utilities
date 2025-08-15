using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices;

public class LookupService<TEntity, TResource> : QueryService<TEntity, TResource>, ILookupService<TEntity, TResource>
    where TEntity : BaseLookupEntity<TEntity>
    where TResource : LookupResource
{
    private readonly ILookupRepository<TEntity> _lookupRepository;

    public LookupService(
        IMapper<TEntity, TResource> mapper,
        ILookupRepository<TEntity> repository
        ) : base(mapper, repository)
    {
        _lookupRepository = repository;
    }

    public async Task<IEnumerable<LookupResource>> GetListAsync(bool activeOnly = true)
    {
        IEnumerable<TEntity> entities = await _lookupRepository.GetListAsync(activeOnly);
        return entities.Select(_mapper.ToResource).ToList();
    }
}
