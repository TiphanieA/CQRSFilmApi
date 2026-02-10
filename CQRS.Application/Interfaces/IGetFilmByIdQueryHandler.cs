using CQRS.Application.Dtos;
using CQRS.Application.Queries;

namespace CQRS.Application.Interfaces;

public interface IGetFilmByIdQueryHandler
{
    Task<FilmDto> Handle(GetFilmByIdQuery request, CancellationToken cancellationToken);
}