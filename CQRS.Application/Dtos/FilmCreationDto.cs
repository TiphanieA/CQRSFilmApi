using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CQRS.Shared.Attributes;
using CQRS.Shared.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace CQRS.Application.Dtos;

[SwaggerSchema("FilmCreationDto")]
public class FilmCreationDto
{
    [SwaggerSchema("Titre")]
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    public string Titre { get; set; }
    
    [SwaggerSchema("Annee")]
    [Required(ErrorMessage = "L'année est requise.")]
    public int Annee { get; set; }
    
    [SwaggerSchema("Identifiant du réalisateur")]
    [Required(ErrorMessage = "Le réalisateur est obligatoire.")]
    [GuidNotEmpty(ErrorMessage = "Le réalisateur est obligatoire.")]
    public Guid RealisateurId { get; set; }

    [SwaggerSchema("Acteurs")] 
    public List<Guid> Acteurs { get; set; } 
    
    [SwaggerSchema("Genre")]
    [Required(ErrorMessage = "Le genre est requis.")]
    public Genre Genre { get; set; } 
    
    [SwaggerSchema("Budget")]
    [Required(ErrorMessage = "Le budget est requis")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Le budget doit être supérieur à 0.")]
    public int Budget { get; set; }

    public FilmCreationDto(string titre, int annee, Guid realisateurId, List<Guid> acteurs, Genre genre, int budget)
    {
        Titre = titre;
        Annee = annee;
        RealisateurId = realisateurId;
        Genre = genre;
        Budget = budget;
        Acteurs = acteurs;
    }
}