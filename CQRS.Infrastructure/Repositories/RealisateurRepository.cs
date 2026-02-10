using Microsoft.EntityFrameworkCore;
using CQRS.Application.Interfaces;

namespace CQRS.Infrastructure.Repositories;

public class RealisateurRepository: IRealisateurRepository
{
    private readonly FilmDbContext _context;
    
    public RealisateurRepository(FilmDbContext context)
    {
        _context = context;
    }
    public Task<bool> RealisateurExistAsync(Guid realisateurId, CancellationToken cancellationToken = default)
        => _context.Realisateurs.AnyAsync(x =>x.Id == realisateurId, cancellationToken);
}