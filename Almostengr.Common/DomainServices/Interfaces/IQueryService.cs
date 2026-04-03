using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryService<TEntity, TResource> where TEntity : Entity where TResource : Resource
{
    Task<IEnumerable<TResource>> GetListAsync(bool sortDescending = false);
    Task<IEnumerable<TEntity>> GetEntityListAsync(bool sortDescending = false);
    Task<bool> ExistsByPublicIdAsync(Guid publicId);
    Task<TResource> GetByGuidAsync(Guid publicId);
    Task<TEntity> GetEntityByPublicIdAsync(Guid publicId);
}
