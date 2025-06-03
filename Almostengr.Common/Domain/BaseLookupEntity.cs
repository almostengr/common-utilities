using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public abstract class BaseLookupEntity : BaseEntity
{
    private BaseLookupEntity(Guid guid, string shortDescription, string fullDescription)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = true;
    }

    protected BaseLookupEntity() : base() { }

    [Required, StringLength(100)]
    public string ShortDescription { get; private set; }

    [StringLength(500)]
    public string FullDescription { get; private set; }

    public bool IsActive { get; private set; }

    public Result<BaseLookupEntity> Create(
        Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy);
    // {
    //     BaseLookupEntity entity = new(guid, shortDescription, fullDescription);
    //     Result<BaseLookupEntity> result = entity.SetModified(modifiedBy);
    //     return result;
    // }

    public Result<BaseLookupEntity> Update(string shortDescription, string fullDescription, bool isActive, string modifiedBy)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SetModified(modifiedBy);

        return Result<BaseLookupEntity>.Success(this);
    }
}
