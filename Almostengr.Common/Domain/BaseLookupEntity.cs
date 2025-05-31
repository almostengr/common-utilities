using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public abstract class BaseLookupEntity : BaseEntity
{
    protected BaseLookupEntity() : base() { }

    [Required, StringLength(100)]
    public string ShortDescription { get; private set; }

    [StringLength(500)]
    public string FullDescription { get; private set; }

    public bool IsActive { get; private set; }

    public abstract Result<BaseLookupEntity> Create(Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy);

    public Result<BaseLookupEntity> Update(string shortDescription, string fullDescription, bool isActive, string modifiedBy)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SetModified(modifiedBy);

        return Result<BaseLookupEntity>.Success(this);
    }
}
