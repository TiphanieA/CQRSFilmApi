using Microsoft.OpenApi.Extensions;
using CQRS.Domain.Entities;
using CQRS.Shared.Enums;

namespace CQRS.Infrastructure.Sort.Films;

public class FilmSorter
{
    private readonly Dictionary<FilmSortBy, IFilmSort> _filmSorting = new();
    
    public FilmSorter()
    {
        _filmSorting.Add(FilmSortBy.Titre, new FilmSortTitre());
        _filmSorting.Add(FilmSortBy.Budget, new FilmSortBudget());
        _filmSorting.Add(FilmSortBy.Annee, new FilmSortAnnee());
    }

    public IQueryable<Film> Sort(IQueryable<Film> query, FilmSortBy sortBy = FilmSortBy.Titre, SortDirection sortDirection = SortDirection.Asc)
    {
        if (!_filmSorting.TryGetValue(sortBy, out var filmSorting))
        {
             throw new ArgumentException($"Tri non supporté : {sortBy.GetDisplayName()}");
        }
        
        return filmSorting.ApplySort(query, sortDirection);
    }
}