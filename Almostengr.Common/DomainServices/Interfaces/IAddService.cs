using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.Common.DomainServices.Interfaces;

public interface IAddService<TEntity, TResource> : ICommandService<TResource> 
    where TResource : BaseResource, new() 
    where TEntity : BaseEntity, new();
