using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class StateProvince : BaseLookupEntity<StateProvince>
{
    private StateProvince() { }

    private StateProvince(Guid guid) : base(guid) { }

    public override Result<StateProvince> Create(Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy)
    {
        StateProvince stateProvince = new(guid);
        return stateProvince.Update(shortDescription, fullDescription, true, modifiedBy);
    }
}
