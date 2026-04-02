using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices;

public class QueryService<TEntity, TResource> : IQueryService<TEntity, TResource>
    where TEntity : Entity where TResource : Resource
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

    public virtual async Task<IEnumerable<TResource>> GetListAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetListAsync();
        return entities.Select(_mapper.ToResource).ToList();
    }

    public virtual async Task<bool> ExistsByGuidAsync(Guid publicId)
    {
        return await _repository.ExistsByPublicIdAsync(publicId);
    }

    public virtual async Task<TResource> GetByGuidAsync(Guid publicId)
    {
        TEntity entity = await _repository.GetByPublicIdAsync(publicId);
        return _mapper.ToResource(entity);
    }

    public async Task<IEnumerable<TEntity>> GetEntityListAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetListAsync();
        return entities;
    }

    public async Task<TEntity> GetEntityByGuidAsync(Guid publicId)
    {
        TEntity entity = await _repository.GetByPublicIdAsync(publicId);
        return entity;
    }
}
