using Almostengr.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IDbContext : IDbContextOptions
{
    DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
