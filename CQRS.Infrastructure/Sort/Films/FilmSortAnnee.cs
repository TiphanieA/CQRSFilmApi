using CQRS.Domain.Entities;
using CQRS.Shared.Enums;

namespace CQRS.Infrastructure.Sort.Films;

public class FilmSortAnnee : IFilmSort
{
    public IQueryable<Film> ApplySort(IQueryable<Film> query, SortDirection sortDirection)
        => sortDirection == SortDirection.Desc ? query.OrderByDescending(f => f.Annee) : query.OrderBy(f => f.Annee);
}