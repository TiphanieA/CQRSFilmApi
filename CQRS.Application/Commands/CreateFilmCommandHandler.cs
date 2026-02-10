using CQRS.Application.Dtos;
using CQRS.Application.Interfaces;
using Microsoft.OpenApi.Extensions;
using CQRS.Domain.Entities;
using CQRS.Shared.Exceptions;

namespace CQRS.Application.Commands;

public class CreateFilmCommandHandler : ICreateFilmCommandHandler
{
    private readonly IFilmRepository _filmRepository;
    private readonly IActeurRepository _acteurRepository;
    private readonly IRealisateurRepository _realisateurRepository;
    
    public CreateFilmCommandHandler(IFilmRepository filmRepository, IActeurRepository acteurRepository, IRealisateurRepository realisateurRepository)
    {
        _filmRepository = filmRepository;
        _acteurRepository = acteurRepository;
        _realisateurRepository = realisateurRepository;
    }
    public async Task<FilmDto> Handle(CreateFilmCommand command, CancellationToken cancellationToken)
    {
        if (!CheckValidityAnneeFilm(command.Annee))
        {
            throw new UnproccessableEntityException("L'année de sortie du film est invalide.");
        }
        
        await CheckIfRealisateurExist(command.RealisateurId, cancellationToken);
        
        var filmExist = await _filmRepository.AnyFilmExistAsync(command.Titre, command.Annee, command.RealisateurId, cancellationToken);

        if (filmExist)
        {
            throw new ExistingEntityException("Le film existe déjà");
        }
        
        var acteurs = await GetActeurs(command.Acteurs, cancellationToken);
        var film = new Film(command.Titre, command.Annee, command.RealisateurId, command.Budget, command.Genre);
        film.AddActeurs(acteurs);
        
        await _filmRepository.AddAsync(film, command.Acteurs, cancellationToken);

        return new FilmDto(film.Id, film.Titre, film.Annee, film.Acteurs.Select(x => $"{x.Prenom} {x.Nom}").ToList(),
            film.Budget, film.RealisateurId, film.Genre);
    }

    private bool CheckValidityAnneeFilm(int annee)
        => annee >= 1895 && annee < DateTime.Now.AddYears(1).Year;

    private async Task<List<Acteur>> GetActeurs(List<Guid> acteurIds, CancellationToken cancellationToken)
    {
        if (!await _acteurRepository.AnyActeursExist(acteurIds, cancellationToken))
        {
            throw new UnproccessableEntityException("Un ou plusieurs acteurs ne sont pas présents en base de données.");
        }

        return await _acteurRepository.GetActeursByIdsAsync(acteurIds, cancellationToken);
    }

    private async Task CheckIfRealisateurExist(Guid realisateurId, CancellationToken cancellationToken)
    {
        if (!await _realisateurRepository.RealisateurExistAsync(realisateurId, cancellationToken))
        {
            throw new UnproccessableEntityException("Le réalisateur n'est pas présent dans la base de données.");
        }
    }
}