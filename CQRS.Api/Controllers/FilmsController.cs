using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Commands;
using CQRS.Application.Dtos;
using CQRS.Application.Interfaces;
using CQRS.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/v1/films")]
public class FilmsController : ControllerBase
{
    private readonly IGetFilmsQueryHandler _getFilmsQueryHandler;
    
    public FilmsController(IGetFilmsQueryHandler getFilmsQueryHandler)
    {
        _getFilmsQueryHandler = getFilmsQueryHandler;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Liste les films d'un réalisateur",
        Description = "Liste les films d'un réalisateur",
        OperationId = "GetFilms",
        Tags = new[] { "Films" }
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Les films concernés ont été retounés", typeof(FilmsDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Les critères ne sont pas valides")]
    public async Task<ActionResult<FilmsDto>> GetFilms([FromQuery] FilmsCriteresTriDto filmsCriteresTri, CancellationToken cancellationToken)
    {
        var getFilmsByRealisateurQuery = new GetFilmsQuery(filmsCriteresTri.RealisateurId, filmsCriteresTri.PageNumber, filmsCriteresTri.PageSize, filmsCriteresTri.SortBy, filmsCriteresTri.SortDirection);
        
        var filmDto = await _getFilmsQueryHandler.Handle(getFilmsByRealisateurQuery, cancellationToken);
        
        return Ok(filmDto);
    }
}