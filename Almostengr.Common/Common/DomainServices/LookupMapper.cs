using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.Common.Common.DomainServices;

public abstract class LookupMapper<TEntity> : ILookupMapper<TEntity, LookupResource> 
    where TEntity : LookupEntity<TEntity>
{
    public LookupResource ToResource(TEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new LookupResource
        {
            PublicId = entity.PublicId,
            IsActive = entity.IsActive,
            ShortDescription = entity.ShortDescription,
            FullDescription = entity.FullDescription,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedBy = entity.ModifiedBy,
            ModifiedDate = entity.ModifiedDate,
        };
    }

    public KeyValuePair<int, string> ToKeyValuePair(TEntity entity)
    {
        if (entity == null)
        {
            return new KeyValuePair<int, string>();
        }

        string displayName = string.IsNullOrWhiteSpace(entity.FullDescription) ?
            entity.ShortDescription : $"{entity.ShortDescription} ({entity.FullDescription})";
        return new KeyValuePair<int, string>(entity.Id, displayName);
    }
}
