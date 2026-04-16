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

    public virtual async Task<IEnumerable<TResource>> GetListAsync(bool sortDescending = false)
    {
        IEnumerable<TEntity> entities = await GetEntityListAsync(sortDescending);
        return entities.Select(_mapper.ToResource).ToList();
    }

    public virtual async Task<bool> ExistsByPublicIdAsync(Guid publicId)
    {
        return await _repository.ExistsByPublicIdAsync(publicId);
    }

    public virtual async Task<TResource> GetByPublicIdAsync(Guid publicId)
    {
        TEntity entity = await GetEntityByPublicIdAsync(publicId);
        return _mapper.ToResource(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetEntityListAsync(bool sortDescending = false)
    {
        IEnumerable<TEntity> entities = await _repository.GetListAsync(sortDescending);
        return entities;
    }

    public virtual async Task<TEntity> GetEntityByPublicIdAsync(Guid publicId)
    {
        TEntity entity = await _repository.GetByPublicIdAsync(publicId);
        return entity;
    }

    public virtual async Task<bool> ExistsByIdAsync(int id)
    {
        bool exist = await _repository.ExistsByIdAsync(id);
        return exist;
    }

    public virtual async Task<TEntity> GetEntityByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity;
    }
}
