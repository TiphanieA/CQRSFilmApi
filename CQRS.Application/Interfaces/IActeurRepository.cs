using CQRS.Domain.Entities;

namespace CQRS.Application.Interfaces;

public interface IActeurRepository
{
    Task<bool> AnyActeursExist(List<Guid> acteursIds, CancellationToken cancellationToken = default);
    
    Task<List<Acteur>> GetActeursByIdsAsync(List<Guid> acteursIds, CancellationToken cancellationToken = default);
}