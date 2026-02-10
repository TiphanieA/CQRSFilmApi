using CQRS.Domain.Entities;
using CQRS.Shared.Enums;

namespace CQRS.Infrastructure.Sort.Films;

public class FilmSortTitre : IFilmSort
{
    public IQueryable<Film> ApplySort(IQueryable<Film> query, SortDirection sortDirection)
        => sortDirection == SortDirection.Desc ? query.OrderByDescending(f => f.Titre) : query.OrderBy(f => f.Titre);
}