using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices;

public abstract class LookupMapper<TEntity> : ILookupMapper<TEntity, LookupResource> where TEntity : BaseLookupEntity<TEntity>
{
    public LookupResource ToResource(TEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new LookupResource
        {
            Guid = entity.Guid,
            IsActive = entity.IsActive,
            ShortDescription = entity.ShortDescription,
            FullDescription = entity.FullDescription,
            ModifiedBy = entity.ModifiedBy,
            ModifiedDate = entity.ModifiedDate,
        };
    }
}
