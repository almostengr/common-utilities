using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class StateProvince : LookupEntity<StateProvince>
{
    private StateProvince(Guid publicId, string createdBy) : base(publicId, createdBy)
    {
    }

    public override Result<StateProvince> Create(
        Guid publicId, string shortDescription, bool isActive, string createdBy, int sortOrder = 1, string fullDescription = null)
    {
        StateProvince stateProvince = new(publicId, createdBy);
        return stateProvince.Update(shortDescription, true, createdBy, sortOrder, fullDescription);
    }
}
