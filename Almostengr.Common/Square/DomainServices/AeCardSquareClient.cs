using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Almostengr.Common.Square.DomainServices.Interfaces;
using Almostengr.Common.Square.Shared;

namespace Almostengr.Common.Square.DomainServices;

public class AeCardSquareClient : AeSquareClient, ICardSquareClient
{
    public AeCardSquareClient(
        ILogger<AeSquareClient> logger, IOptions<SquareSettings> options) : base(logger, options)
    {
    }

    // public async Task GetOrCreateCardAsync()
}
