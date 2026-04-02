using System.ComponentModel;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.Common.Domain;

public class Country : LookupEntity<Country>
{
    private Country(Guid publicId, string createdBy) : base(publicId, createdBy)
    {
    }

    public override Result<Country> Create(
        Guid publicId, string shortDescription, bool isActive, string createdBy, int sortOrder = 1, string fullDescription = null)
    {
        Country country = new(publicId, createdBy);
        return country.Update(shortDescription, isActive, createdBy, sortOrder, fullDescription);
    }
}
