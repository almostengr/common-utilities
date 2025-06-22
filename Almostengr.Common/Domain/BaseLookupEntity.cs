using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public abstract class BaseLookupEntity<T> : BaseEntity where T : BaseLookupEntity<T>
{
    protected BaseLookupEntity(Guid guid, string shortDescription, string fullDescription) : base(guid)
    {
        Guid = guid;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = true;
    }

    protected BaseLookupEntity(Guid guid) : base(guid)
    {
        Guid = guid;
    }

    protected BaseLookupEntity() : base() { }

    [Required, StringLength(LibConstants.ShortLength)]
    public string ShortDescription { get; protected set; }

    [StringLength(LibConstants.LongLength)]
    public string FullDescription { get; protected set; }

    public bool IsActive { get; protected set; }

    public abstract Result<T> Create(
        Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy);

    public Result<T> Update(string shortDescription, bool isActive, string modifiedBy)
    {
        ShortDescription = shortDescription;
        IsActive = isActive;
        SetModified(modifiedBy);
        return Result<T>.Success((T)this);
    }

    public Result<T> Update(string shortDescription, string fullDescription, bool isActive, string modifiedBy)
    {
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SetModified(modifiedBy);

        return Result<T>.Success((T)this);
    }
}
