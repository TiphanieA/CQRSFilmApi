using Microsoft.EntityFrameworkCore;
using CQRS.Application.Interfaces;
using CQRS.Domain.Entities;

namespace CQRS.Infrastructure.Repositories;

public class ActeurRepository : IActeurRepository
{
    private readonly FilmDbContext _context;
    
    public ActeurRepository(FilmDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AnyActeursExist(List<Guid> acteursIds, CancellationToken cancellationToken = default)
    {
        var count = await _context.Acteurs
            .Where(a => acteursIds.Contains(a.Id))
            .CountAsync(cancellationToken);

        return count == acteursIds.Count;
    }
    
    public async Task<List<Acteur>> GetActeursByIdsAsync(List<Guid> acteursIds, CancellationToken cancellationToken = default)
    {
        var acteurs = await _context.Acteurs
            .Where(a => acteursIds.Contains(a.Id))
            .ToListAsync(cancellationToken);

        return acteurs;
    }
}