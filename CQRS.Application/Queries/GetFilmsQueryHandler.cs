using CQRS.Application.Dtos;
using CQRS.Application.Interfaces;
using Microsoft.OpenApi.Extensions;

namespace CQRS.Application.Queries;

public class GetFilmsQueryHandler : IGetFilmsQueryHandler
{
    private readonly IFilmRepository _filmRepository;
    public GetFilmsQueryHandler(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }
    
    public async Task<FilmsDto> Handle(GetFilmsQuery request, CancellationToken cancellationToken)
    {
        var films = await _filmRepository.GetFilmsAsync(request, cancellationToken);

        return new FilmsDto()
        {
            Films = films.Items.Select(f => new FilmDto(f.Id, f.Titre, f.Annee, f.Acteurs.Select(x => $"{x.Prenom} {x.Nom}").ToList(),
                f.Budget, f.RealisateurId, f.Genre)).ToList(),
            TotalItems = films.TotalItems,
            PageSize = films.PageSize,
            PageNumber = films.PageNumber,
        };
    }
}