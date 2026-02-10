using System.ComponentModel.DataAnnotations;

namespace CQRS.Shared.Enums;

public enum Genre
{
    [Display(Name = "Aucun")]
    Aucun = 0,
    
    [Display(Name = "Action")]
    Action = 1,
    
    [Display(Name = "Comédie")]
    Comedie = 2,
    
    [Display(Name = "Drame")]
    Drame = 3,
    
    [Display(Name = "Horreur")]
    Horreur = 4,
    
    [Display(Name = "Science-Fiction")]
    ScienceFiction = 5,
}