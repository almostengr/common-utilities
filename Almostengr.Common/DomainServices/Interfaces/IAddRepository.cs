using Almostengr.Common.Domain;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAddRepository<TEntity> : IQueryRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task SaveChangesAsync();
    Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync();
}
