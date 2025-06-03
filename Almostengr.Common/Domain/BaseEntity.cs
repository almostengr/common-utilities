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

    protected Result<T> SetModified<T>(string modifiedBy) where T : BaseEntity
    {
        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            return Result<T>.Failure("Modified By was not provided.");
        }

        ModifiedDate = DateTime.UtcNow;
        ModifiedBy = modifiedBy;
        return Result<T>.Success((T)this);
    }
}
