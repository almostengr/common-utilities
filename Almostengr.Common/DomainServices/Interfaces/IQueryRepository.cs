using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryRepository<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetListAsync();
    Task<TEntity> GetByPublicIdAsync(Guid publicId);
    Task<bool> ExistsByPublicIdAsync(Guid publicId);
    Task<TEntity> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}
