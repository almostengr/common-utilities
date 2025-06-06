using System.ComponentModel.DataAnnotations;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public abstract class BaseEntity
{
    protected BaseEntity() { }

    protected BaseEntity(Guid guid, string modifiedBy)
    {
        Guid = guid;
        ModifiedDate = DateTime.UtcNow;
        ModifiedBy = modifiedBy;
    }

    [Key]
    public int Id { get; protected set; }

    public Guid Guid { get; protected set; }

    [Required, MaxLength(100)]
    public string ModifiedBy { get; protected set; }

    public DateTime ModifiedDate { get; protected set; }

    protected void SetModified(string modifiedBy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifiedBy, nameof(modifiedBy));

        ModifiedDate = DateTime.UtcNow;
        ModifiedBy = modifiedBy;
    }
}
