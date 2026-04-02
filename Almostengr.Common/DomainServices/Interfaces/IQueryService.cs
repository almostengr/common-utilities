using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryService<TEntity, TResource> where TEntity : Entity where TResource : Resource
{
    Task<IEnumerable<TResource>> GetListAsync();
    Task<IEnumerable<TEntity>> GetEntityListAsync();
    Task<bool> ExistsByGuidAsync(Guid publicId);
    Task<TResource> GetByGuidAsync(Guid publicId);
    Task<TEntity> GetEntityByGuidAsync(Guid publicId);
}
