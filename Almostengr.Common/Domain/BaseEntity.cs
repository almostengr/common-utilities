using System.ComponentModel.DataAnnotations;

namespace Almostengr.Common.Domain;

public abstract class BaseEntity
{
    protected BaseEntity() { }

    protected BaseEntity(Guid guid, string modifiedBy) : this(guid)
    {
        SetModified(modifiedBy);
    }

    protected BaseEntity(Guid guid)
    {
        Guid = guid;
    }

    [Key]
    public int Id { get; protected set; }

    public Guid Guid { get; protected set; }

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
