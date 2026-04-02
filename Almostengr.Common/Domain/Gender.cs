using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class Gender : LookupEntity<Gender>
{
    private Gender(Guid publicId, string createdBy) : base(publicId, createdBy)
    {
    }

    public override Result<Gender> Create(
        Guid publicId, string shortDescription, bool isActive, string createdBy, int sortOrder = 1, string fullDescription = null)
    {
        Gender gender = new(publicId, createdBy);
        return gender.Update(shortDescription, isActive, createdBy, sortOrder, fullDescription);
    }
}