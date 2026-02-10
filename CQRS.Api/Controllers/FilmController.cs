using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Commands;
using CQRS.Application.Dtos;
using CQRS.Application.Interfaces;
using CQRS.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/v1/films")]
public class FilmController  : ControllerBase
{
    private readonly ICreateFilmCommandHandler _createFilmCommandHandler;
    private readonly IGetFilmByIdQueryHandler _getFilmByIdQueryHandler;
    
    public FilmController(ICreateFilmCommandHandler createFilmCommandHandler, IGetFilmByIdQueryHandler getFilmByIdQueryHandler)
    {
        _getFilmByIdQueryHandler = getFilmByIdQueryHandler;
        _createFilmCommandHandler = createFilmCommandHandler;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Création d'un film",
        Description = "Création d'un film avec les paramètres donnés",
        OperationId = "CreateFilm",
        Tags = new[] { "Films" }
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Le film a été crée")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Le film n'est pas valide")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Le film existe déjà en base de données")]
    public async Task<ActionResult<FilmDto>> CreateFilm([FromBody] FilmCreationDto filmCreation, CancellationToken cancellationToken)
    {
        var createFilmCommand = new CreateFilmCommand(filmCreation.Titre, filmCreation.Annee, filmCreation.RealisateurId, filmCreation.Acteurs, filmCreation.Genre, filmCreation.Budget);
        
        var filmDto = await _createFilmCommandHandler.Handle(createFilmCommand, cancellationToken);
        
        return CreatedAtAction(nameof(GetFilm), new { id = filmDto.Id }, filmDto);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Récupère un film par son id",
        Description = "Récupère un film par son id",
        OperationId = "GetFilm",
        Tags = new[] { "Films" }
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Le film a été trouvé", typeof(FilmDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Le film n'a pas été trouvé")]
    public async Task<ActionResult<FilmDto>> GetFilm([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var getFilmByIdQuery = new GetFilmByIdQuery(id);
        
        var film = await _getFilmByIdQueryHandler.Handle(getFilmByIdQuery, cancellationToken);
        
        return Ok(film);
    }
}