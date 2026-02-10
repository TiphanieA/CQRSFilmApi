namespace CQRS.Application.Interfaces;

public interface IRealisateurRepository
{
    Task<bool> RealisateurExistAsync(Guid realisateurId, CancellationToken cancellationToken = default);
}