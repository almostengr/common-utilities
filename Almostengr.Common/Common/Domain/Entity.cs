using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Common.Shared;

namespace Almostengr.Common.Common.Domain;

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

    [Required, MaxLength(AeConstants.MediumLength)]
    public string CreatedBy { get; protected set; }

    public DateTimeOffset CreatedDate { get; protected set; }

    [Required, MaxLength(AeConstants.MediumLength)]
    public string ModifiedBy { get; protected set; }

    public DateTimeOffset ModifiedDate { get; protected set; }

    protected void SetModified(string modifiedBy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifiedBy, nameof(modifiedBy));

        ModifiedDate = DateTimeOffset.Now;
        ModifiedBy = modifiedBy;
    }
}
