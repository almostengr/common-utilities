namespace Almostengr.Common.Domain;

public class ApiKey : BaseEntity
{
    public string Key { get; private set; }
    public bool IsActive { get; private set; }
    public int UserId { get; private set; }
}
