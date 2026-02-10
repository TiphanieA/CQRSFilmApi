using System.ComponentModel.DataAnnotations;

namespace CQRS.Shared.Enums;

public enum SortDirection
{
    [Display(Name = "Ascendant")]
    Asc = 0,
    
    [Display(Name = "Descendant")]
    Desc = 1,
}