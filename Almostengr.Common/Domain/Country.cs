using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class Country : BaseLookupEntity<Country>
{
    private Country() { }

    private Country(Guid guid) : base(guid) { }

    public override Result<Country> Create(Guid guid, string shortDescription, string fullDescription, bool isActive, string modifiedBy, int sortOrder = 1)
    {
        Country country = new(guid);
        return country.Update(shortDescription,  true, modifiedBy, sortOrder, fullDescription);
    }
}
