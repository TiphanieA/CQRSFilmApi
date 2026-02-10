
using CQRS.Application.Dtos;
using CQRS.Application.Interfaces;
using Microsoft.OpenApi.Extensions;

namespace CQRS.Application.Queries;

public class GetFilmByIdQueryHandler : IGetFilmByIdQueryHandler
{
    private readonly IFilmRepository _filmRepository;
    public GetFilmByIdQueryHandler(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }
    
    public async Task<FilmDto> Handle(GetFilmByIdQuery request, CancellationToken cancellationToken)
    {
        var film = await _filmRepository.GetFilmByIdAsync(request.FilmId, cancellationToken);
        
        return new FilmDto(film.Id, film.Titre, film.Annee, film.Acteurs.Select(x => $"{x.Prenom} {x.Nom}").ToList(),
            film.Budget, film.RealisateurId, film.Genre);
    }
}