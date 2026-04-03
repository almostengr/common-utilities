using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Shared;

namespace Almostengr.Common.Domain;

public abstract class Entity
{
    protected Entity() { }

    protected Entity(Guid publicId, string createdBy) : base()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(createdBy, nameof(createdBy));

        PublicId = publicId == Guid.Empty ? Guid.NewGuid() : publicId;
        CreatedBy = createdBy;
        CreatedDate = DateTime.UtcNow;
        SetModified(createdBy);
    }

    [Key]
    public int Id { get; protected set; }

    public Guid PublicId { get; protected set; }

    [Required, MaxLength(LibConstants.MediumLength)]
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
