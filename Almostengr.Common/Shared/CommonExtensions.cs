using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;
using Almostengr.Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Almostengr.Common.Shared;

public static class CommonExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IAddRepository<>), typeof(AddRepository<>));
        services.AddTransient(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));
        services.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        services.AddTransient(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddTransient(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));
        services.AddTransient(typeof(IQueryLookupRepository<>), typeof(QueryLookupRepository<>));

        services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        services.AddTransient(typeof(IQueryService<,>), typeof(QueryService<,>));
        services.AddTransient(typeof(IQueryLookupService<,>), typeof(QueryLookupService<,>));
    }

    public static TResource ToResource<TEntity, TResource>(this TEntity entity)
        where TEntity : BaseEntity where TResource : BaseResource, new()
    {
        if (entity == null)
        {
            return null;
        }

        return new TResource
        {
            Guid = entity.Guid,
            ModifiedBy = entity.ModifiedBy,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public static LookupResource ToLookupResource<TEntity>(this TEntity entity)
        where TEntity : BaseLookupEntity
    {
        if (entity == null)
        {
            return null;
        }

        return new LookupResource
        {
            Guid = entity.Guid,
            ModifiedBy = entity.ModifiedBy,
            ModifiedDate = entity.ModifiedDate,
            IsActive = entity.IsActive,
            ShortDescription = entity.ShortDescription,
            FullDescription = entity.FullDescription,
        };
    }
}
