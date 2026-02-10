using CQRS.Domain.Entities;
using CQRS.Shared.Enums;

namespace CQRS.Infrastructure.Sort.Films;

public interface IFilmSort
{
    IQueryable<Film> ApplySort(IQueryable<Film> query, SortDirection sortDirection);
}