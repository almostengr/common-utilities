using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Common.Shared;

namespace Almostengr.Common.Domain;

public abstract class LookupEntity<T> : Entity where T : LookupEntity<T>
{
    protected LookupEntity(
        Guid publicId, string shortDescription, bool isActive, string createdBy, int sortOrder, string fullDescription
        ) : base(publicId, createdBy)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        SortOrder = sortOrder;
        IsActive = isActive;
    }

    protected LookupEntity() : base() { }

    protected LookupEntity(Guid publicId, string createdBy) : base(publicId, createdBy)
    {
    }

    [Required, StringLength(LibConstants.ShortLength)]
    public string ShortDescription { get; protected set; }

    [StringLength(LibConstants.LongLength)]
    public string FullDescription { get; protected set; }

    public bool IsActive { get; protected set; }
    public int SortOrder { get; protected set; } = 1;

    public abstract Result<T> Create(
        Guid publicId, string shortDescription, bool isActive, string createdBy, int sortOrder = 1, string fullDescription = null);

    public Result<T> Update(
        string shortDescription, bool isActive, string modifiedBy, int sortOrder = 1, string fullDescription = null)
    {
        ShortDescription = shortDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        FullDescription = fullDescription;
        SetModified(modifiedBy);
        return Result<T>.Success((T)this);
    }
}
