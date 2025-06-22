using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class Gender : BaseLookupEntity<Gender>
{
    public Gender(Guid guid) : base(guid)
    {
    }

    public override Result<Gender> Create(Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy)
    {
        Gender gender = new(guid);
        return gender.Update(shortDescription, fullDescription, isActive, modifiedBy);
    }
}