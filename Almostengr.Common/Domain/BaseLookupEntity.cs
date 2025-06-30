using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Common.Shared;

namespace Almostengr.Common.Domain;

public abstract class BaseLookupEntity<T> : BaseEntity where T : BaseLookupEntity<T>
{
    protected BaseLookupEntity(Guid guid, string shortDescription, string fullDescription) : base(guid)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
    }

    protected BaseLookupEntity(Guid guid) : base(guid)
    {
        Guid = guid;
        IsActive = true;
    }

    protected BaseLookupEntity() : base() { }

    [Required, StringLength(LibConstants.ShortLength)]
    public string ShortDescription { get; protected set; }

    [StringLength(LibConstants.LongLength)]
    public string FullDescription { get; protected set; }

    public bool IsActive { get; protected set; }
    public int SortOrder { get; protected set; } = 1;

    public abstract Result<T> Create(
        Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy, int sortOrder = 1);

    public Result<T> Update(string shortDescription, bool isActive, string modifiedBy, int sortOrder = 1)
    {
        ShortDescription = shortDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        SetModified(modifiedBy);
        return Result<T>.Success((T)this);
    }

    public Result<T> Update(string shortDescription, string fullDescription, bool isActive, string modifiedBy, int sortOrder = 1)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        SetModified(modifiedBy);

        return Result<T>.Success((T)this);
    }
}
