using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CQRS.Shared.Enums;

public enum FilmSortBy
{
    [Display(Name = "Titre")]
    Titre = 0,

    [Display(Name = "Budget")]
    Budget = 1,
    
    [Display(Name = "Annee")]
    Annee = 2
}