namespace Almostengr.Common.DomainServices.Resources;

public abstract class Resource
{
    public Guid PublicId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}
