using CQRS.Application.Commands;
using CQRS.Application.Dtos;

namespace CQRS.Application.Interfaces;

public interface ICreateFilmCommandHandler
{
    Task<FilmDto> Handle(CreateFilmCommand command, CancellationToken cancellationToken);
}