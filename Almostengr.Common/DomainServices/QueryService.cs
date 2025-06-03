using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.Shared;

namespace Almostengr.Common.DomainServices;

public class QueryService<TEntity, TResource> : IQueryService<TEntity, TResource>
    where TEntity : BaseEntity where TResource : BaseResource
{
    protected readonly IQueryRepository<TEntity> _repository;

    public QueryService(IQueryRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TResource>> GetListAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetListAsync();
        return entities.Select(e => e.ToResource<TEntity, TResource>()).ToArray();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _repository.ExistsByIdAsync(id);
    }

    public async Task<TResource> GetByIdAsync(int id)
    {
        TEntity entity = await _repository.GetByIdAsync(id);
        return entity.ToResource<TEntity, TResource>();
    }

    public async Task<bool> ExistsByGuidAsync(Guid guid)
    {
        return await _repository.ExistsByGuidAsync(guid);
    }

    public async Task<TResource> GetByGuidAsync(Guid guid)
    {
        TEntity entity = await _repository.GetByGuidAsync(guid);
        return entity.ToResource<TEntity, TResource>();
    }
}
