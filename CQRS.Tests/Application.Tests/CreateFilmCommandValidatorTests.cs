using System.ComponentModel.DataAnnotations;
using CQRS.Application.Dtos;
using CQRS.Shared.Enums;

namespace CQRS.Tests.Application.Tests;

public class CreateFilmCommandValidatorTests
{
    [Theory]
    [InlineData("kill bill", 2003, 0, Genre.Action, "29db4635-020e-4c0a-8075-91d276fe719d","Le budget doit être supérieur à 0.")]
    public async Task ShouldGenerateValidationError_WhenZeroBudget(string titre, int annee, int budget, Genre genre,  Guid realisateurId, string validationErrorMessage)
        => await CheckMessageValidationErreur(titre, annee, budget, genre, realisateurId, validationErrorMessage);
    
    [Theory]
    [InlineData(null, 2003, 200000, Genre.Action, "29db4635-020e-4c0a-8075-91d276fe719d", "Le titre est obligatoire.")]
    [InlineData("", 2003, 200000, Genre.Action,"29db4635-020e-4c0a-8075-91d276fe719d", "Le titre est obligatoire.")]
    public async Task ShouldGenerateValidationError_WhenInvalidTitre(string titre, int annee, int budget,Genre genre, Guid realisateurId, string validationErrorMessage)
        => await CheckMessageValidationErreur(titre, annee, budget, genre, realisateurId, validationErrorMessage);

    [Theory]
    [InlineData("kill bill", 2003, 2000000, Genre.Action, null, "Le réalisateur est obligatoire.")]
    public async Task ShouldGenerateValidationError_WhenNoRealisateur(string titre, int annee, int budget, Genre genre, Guid realisateurId, string validationErrorMessage)
        => await CheckMessageValidationErreur(titre, annee, budget, genre, realisateurId, validationErrorMessage);
    
    [Theory]
    [InlineData("kill bill", 2003, 200000, Genre.Action, "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldReturnCorrect_WhenFilmValid(string titre, int annee, int budget, Genre genre, Guid realisateurId)
    {
        var filmDto = new FilmCreationDto(titre, annee, realisateurId, new List<Guid>(), genre, budget);
        
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(filmDto, new ValidationContext(filmDto), validationResults, true);
        
        Assert.True(isValid);

        await Task.CompletedTask;
    }
    
    private static async Task CheckMessageValidationErreur(string titre, int annee, int budget, Genre genre, Guid realisateurId, string validationErrorMessage)
    {
        var filmDto = new FilmCreationDto(titre, annee, realisateurId, new List<Guid>(), genre, budget);
        
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(filmDto, new ValidationContext(filmDto), validationResults, true);
        
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == validationErrorMessage);
        
        await Task.CompletedTask;
    }
}