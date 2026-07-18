using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices.Interfaces;

public interface IQueryService<TEntity, TResource> where TEntity : Entity where TResource : Resource
{
    Task<IEnumerable<TResource>> GetListAsync(bool sortDescending = false);
    Task<IEnumerable<TEntity>> GetEntityListAsync(bool sortDescending = false);
    Task<bool> ExistsByPublicIdAsync(Guid publicId);
    Task<TResource> GetByPublicIdAsync(Guid publicId);
    Task<TEntity> GetEntityByPublicIdAsync(Guid publicId);
    Task<bool> ExistsByIdAsync(int id);
    Task<TEntity> GetEntityByIdAsync(int id);
}
