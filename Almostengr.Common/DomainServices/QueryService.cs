using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices;

public class QueryService<TEntity, TResource> : IQueryService<TEntity, TResource>
    where TEntity : BaseEntity where TResource : BaseResource
{
    protected readonly IQueryRepository<TEntity> _repository;
    protected readonly IMapper<TEntity, TResource> _mapper;

    public QueryService(
        IMapper<TEntity, TResource> mapper,
        IQueryRepository<TEntity> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TResource>> GetListAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetListAsync();
        return entities.Select(_mapper.ToResource).ToList();
    }

    public async Task<bool> ExistsByGuidAsync(Guid guid)
    {
        return await _repository.ExistsByGuidAsync(guid);
    }

    public async Task<TResource> GetByGuidAsync(Guid guid)
    {
        TEntity entity = await _repository.GetByGuidAsync(guid);
        return _mapper.ToResource(entity);
    }
}
