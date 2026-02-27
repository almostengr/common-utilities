using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Shared;

namespace Almostengr.Common.Domain;

public abstract class BaseEntity
{
    protected BaseEntity() { }

    protected BaseEntity(Guid guid, string createdBy) : this(guid)
    {
        CreatedBy = createdBy;
        CreatedDate = DateTime.UtcNow;
        SetModified(createdBy);
    }

    protected BaseEntity(Guid guid)
    {
        Guid = guid == Guid.Empty ? Guid.NewGuid() : guid;
    }

    [Key]
    public int Id { get; protected set; }

    public Guid Guid { get; protected set; }

    [Required, StringLength(LibConstants.MediumLength)]
    public string CreatedBy { get; protected set; }

    public DateTime CreatedDate { get; protected set; }

    [Required, MaxLength(LibConstants.MediumLength)]
    public string ModifiedBy { get; protected set; }

    public DateTime ModifiedDate { get; protected set; }

    protected void SetModified(string modifiedBy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifiedBy, nameof(modifiedBy));

        ModifiedDate = DateTime.UtcNow;
        ModifiedBy = modifiedBy;
    }
}
