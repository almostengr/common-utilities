namespace Almostengr.Common.DomainServices.Resources;

public abstract class BaseResource
{
    public BaseResource() { }

    public Guid Guid { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}
