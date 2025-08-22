using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IQueryRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetListAsync();
    Task<TEntity> GetByGuidAsync(Guid guid);
    Task<bool> ExistsByGuidAsync(Guid guid);
    Task<TEntity> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}
