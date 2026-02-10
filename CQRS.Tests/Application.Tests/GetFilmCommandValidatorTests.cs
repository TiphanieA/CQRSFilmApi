using System.ComponentModel.DataAnnotations;
using CQRS.Application.Dtos;
using CQRS.Shared.Enums;

namespace CQRS.Tests.Application.Tests;

public class GetFilmCommandValidatorTests
{
    [Theory]
    [InlineData(null, 1,10,FilmSortBy.Titre, SortDirection.Asc, "Le réalisateur est obligatoire.")]
    public async Task ShouldHaveValidationError_WhenNoRealisateur(Guid realisateurId, int pageNumber, int pageSize, FilmSortBy filmSortBy, SortDirection sortDirection, string validationErrorMessage)
        => await CheckMessageValidationErreur(realisateurId, pageNumber, pageSize, filmSortBy, sortDirection, validationErrorMessage);
    
    private static async Task CheckMessageValidationErreur(Guid realisateurId, int pageNumber, int pageSize, FilmSortBy filmSortBy, SortDirection sortDirection, string validationErrorMessage)
    {
        var filmsCriteresTriDto = new FilmsCriteresTriDto(realisateurId,pageNumber, pageSize, filmSortBy , sortDirection);
        
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(filmsCriteresTriDto, new ValidationContext(filmsCriteresTriDto), validationResults, true);
        
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == validationErrorMessage);
        
        await Task.CompletedTask;
    }
}