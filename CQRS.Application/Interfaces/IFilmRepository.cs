using CQRS.Application.Common;
using CQRS.Application.Queries;
using CQRS.Domain.Entities;
using CQRS.Shared;

namespace CQRS.Application.Interfaces;

public interface IFilmRepository
{
    Task<bool> AnyFilmExistAsync(string titre, int annee, Guid realisateurId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Film film, List<Guid> acteursIds, CancellationToken cancellationToken = default);
    
    Task<PagedResult<Film>> GetFilmsAsync(GetFilmsQuery query, CancellationToken cancellationToken = default);
    
    Task<Film> GetFilmByIdAsync(Guid filmId, CancellationToken cancellationToken = default);
    
}