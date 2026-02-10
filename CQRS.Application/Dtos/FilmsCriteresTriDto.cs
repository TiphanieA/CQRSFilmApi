using System.ComponentModel.DataAnnotations;
using CQRS.Shared.Attributes;
using CQRS.Shared.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace CQRS.Application.Dtos;

[SwaggerSchema("Critères de filtres et de tris des films")]
public class FilmsCriteresTriDto
{
    [SwaggerSchema("Identifiant du réalisateur")]
    [Required(ErrorMessage = "Le réalisateur est obligatoire")]
    [GuidNotEmpty(ErrorMessage = "Le réalisateur est obligatoire.")]
    public Guid RealisateurId { get; set; }
    
    [SwaggerSchema("Numéro de la page en cours")]
    [Required(ErrorMessage = "Le numéro de page en cours est obligatoire")]
    public int PageNumber { get; set; }
    
    [SwaggerSchema("Nombre d'élément par page")]
    [Required(ErrorMessage = "Le nombre d'élément par page est obligatoire")]
    public int PageSize { get; set; }

    [SwaggerSchema("Colonne de tri (0 : Titre, 1 : Budget, 2 : Annee")]
    [Required(ErrorMessage = "La colonne de tri est obligatoire")]
    public FilmSortBy SortBy { get; set; } 
    
    [SwaggerSchema("Ordre de tri (0 : ASC, 1 : DESC)")]
    [Required(ErrorMessage = "Le sens de tri est obligatoire")]
    public SortDirection SortDirection { get; set; } 

    public FilmsCriteresTriDto(Guid realisateurId, int pageNumber, int pageSize, FilmSortBy sortBy, SortDirection sortDirection)
    {
        RealisateurId = realisateurId;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortBy = sortBy;
        SortDirection = sortDirection;
    }

    public FilmsCriteresTriDto()
    {
        
    }
}