namespace Almostengr.Common.Common.DomainServices.Resources;

public abstract class Resource
{
    public Guid PublicId { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}
