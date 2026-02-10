using Microsoft.EntityFrameworkCore;
using CQRS.Application.Common;
using CQRS.Application.Interfaces;
using CQRS.Application.Queries;
using CQRS.Domain.Entities;
using CQRS.Infrastructure.Sort.Films;
using CQRS.Shared.Exceptions;

namespace CQRS.Infrastructure.Repositories;

public class FilmRepository : IFilmRepository 
{
    private readonly FilmDbContext _context;
    private readonly FilmSorter _filmSorter;

    public FilmRepository(FilmDbContext context)
    {
        _context = context;
        _filmSorter = new FilmSorter();
    }
    
    public async Task<bool> AnyFilmExistAsync(string titre, int annee, Guid realisateurId, CancellationToken cancellationToken = default)
        =>  await _context.Films
            .AnyAsync(x => EF.Functions.Like(x.Titre, titre) &&
                      x.Annee == annee && 
                      x.RealisateurId == realisateurId, cancellationToken);
    
    public async Task AddAsync(Film film, List<Guid> acteursIds, CancellationToken cancellationToken = default)
    {
        await _context.Films.AddAsync(film, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedResult<Film>> GetFilmsAsync(GetFilmsQuery query, CancellationToken cancellationToken = default)
    {
        var filmsQuery = _context.Films.Include(x => x.Acteurs).Where(x => x.RealisateurId == query.RealisateurId);

        filmsQuery = _filmSorter.Sort(filmsQuery, query.SortBy, query.SortDirection);

        var totalItems = await filmsQuery.CountAsync(cancellationToken);
        var films = await filmsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize) 
            .ToListAsync(cancellationToken);
        
        return new PagedResult<Film>
        {
            TotalItems = totalItems,
            Items = films,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<Film> GetFilmByIdAsync(Guid filmId, CancellationToken cancellationToken = default)
    {
        var film = await _context.Films.Include(x => x.Acteurs).FirstOrDefaultAsync(x => x.Id == filmId, cancellationToken);

        if (film == null)
            throw new NotFoundException("Le film n'existe pas.");

        return film;
    }
}