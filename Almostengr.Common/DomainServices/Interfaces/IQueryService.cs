using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryService<TEntity, TResource> where TEntity : BaseEntity where TResource : BaseResource
{
    Task<IEnumerable<TResource>> GetListAsync();
    Task<IEnumerable<TEntity>> GetEntityListAsync();
    Task<bool> ExistsByGuidAsync(Guid guid);
    Task<TResource> GetByGuidAsync(Guid guid);
    Task<TEntity> GetEntityByGuidAsync(Guid guid);
}
