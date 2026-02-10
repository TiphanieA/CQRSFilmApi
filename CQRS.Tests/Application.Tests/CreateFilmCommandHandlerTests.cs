using Moq;
using CQRS.Application.Commands;
using CQRS.Application.Interfaces;
using CQRS.Domain.Entities;
using CQRS.Shared.Enums;
using CQRS.Shared.Exceptions;

namespace CQRS.Tests.Application.Tests;

public class CreateFilmCommandHandlerTests
{
    private readonly CreateFilmCommandHandler  _createFilmCommandHandlerMock;
    private readonly Mock<IFilmRepository> _filmRepositoryMock;
    private readonly Mock<IActeurRepository> _acteurRepositoryMock;
    private readonly Mock<IRealisateurRepository> _realisateurRepositoryMock;
    
    public CreateFilmCommandHandlerTests()
    {
        _filmRepositoryMock = new Mock<IFilmRepository>();
        _acteurRepositoryMock = new Mock<IActeurRepository>();
        _realisateurRepositoryMock = new Mock<IRealisateurRepository>();
        _createFilmCommandHandlerMock =new CreateFilmCommandHandler(_filmRepositoryMock.Object, _acteurRepositoryMock.Object, _realisateurRepositoryMock.Object);
    }
    
    [Theory]
    [InlineData("kill bill", 2003, 200000, "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldCreateFilm_WhenFilmIsValid(string titre, int annee, int budget, Guid realisateurId)
    {
        var command = new CreateFilmCommand(titre, annee, realisateurId, new List<Guid>(), Genre.Action, budget);
        var generatedGuid = Guid.NewGuid();
        
        _acteurRepositoryMock.Setup(r => r.AnyActeursExist(It.IsAny<List<Guid>>(), CancellationToken.None))
            .ReturnsAsync(true); 
        
        _acteurRepositoryMock.Setup(r => r.GetActeursByIdsAsync(It.IsAny<List<Guid>>(), CancellationToken.None))
            .ReturnsAsync(new List<Acteur>()); 
        
       _filmRepositoryMock.Setup(r => r.AddAsync( It.IsAny<Film>(), new List<Guid>(), CancellationToken.None))
           .Callback((Film film, List<Guid> users, CancellationToken cancellationToken) => film.Id = generatedGuid)
           .Returns(Task.CompletedTask);
       
       _realisateurRepositoryMock.Setup(r => r.RealisateurExistAsync(realisateurId, CancellationToken.None))
           .ReturnsAsync(true); 

        var filmDto = await _createFilmCommandHandlerMock.Handle(command, CancellationToken.None);
        Assert.Equal(filmDto.Titre, titre);
        Assert.Equal(filmDto.Annee, annee);
        Assert.Equal(filmDto.Budget, budget);
        Assert.Equal(filmDto.RealisateurId, realisateurId);
        Assert.Equal(filmDto.Id, generatedGuid);
    }
    
    [Theory]
    [InlineData("kill bill", 2003, 200000, "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldReturnException_WhenFilmExist(string titre, int annee, int budget, Guid realisateurId)
    {
        var command = new CreateFilmCommand(titre, annee, realisateurId, new List<Guid>(), Genre.Action, budget);
        
        _filmRepositoryMock.Setup(r => r.AnyFilmExistAsync(It.Is<string>(s => s == titre), It.Is<int>(i => i == annee), It.Is<Guid>(g => g == realisateurId), CancellationToken.None))
            .ReturnsAsync(true); 
        
        _realisateurRepositoryMock.Setup(r => r.RealisateurExistAsync(realisateurId, CancellationToken.None))
            .ReturnsAsync(true); 
        
        var exception = await Assert.ThrowsAsync<ExistingEntityException>(async () => await _createFilmCommandHandlerMock.Handle(command, CancellationToken.None));
        Assert.Equal("Le film existe déjà", exception.Message);
    }
    
    [Theory]
    [InlineData("kill bill", 2003, 200000, "29db4635-020e-4c0a-8075-91d276fe719d", "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldReturnException_WhenActeurInvalid(string titre, int annee, int budget, Guid realisateurId, Guid acteurId)
    {
        var command = new CreateFilmCommand(titre, annee, realisateurId, new List<Guid>() {acteurId}, Genre.Action, budget);
        
        _acteurRepositoryMock.Setup(r => r.AnyActeursExist(It.IsAny<List<Guid>>(), CancellationToken.None))
            .ReturnsAsync(false); 
        
        _realisateurRepositoryMock.Setup(r => r.RealisateurExistAsync(realisateurId, CancellationToken.None))
            .ReturnsAsync(true); 
        
        var exception = await Assert.ThrowsAsync<UnproccessableEntityException>(async () => await _createFilmCommandHandlerMock.Handle(command, CancellationToken.None));
        Assert.Equal("Un ou plusieurs acteurs ne sont pas présents en base de données.", exception.Message);
    }
    
    [Theory]
    [InlineData("kill bill", 2026, 200000,  "29db4635-020e-4c0a-8075-91d276fe719d")]
    [InlineData("kill bill", 1800, 200000,  "29db4635-020e-4c0a-8075-91d276fe719d")]
    [InlineData("kill bill", 0, 200000, "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldReturnException_WhenAnneeSortieInvalid(string titre, int annee, int budget, Guid realisateurId)
    {
        var command = new CreateFilmCommand(titre, annee, realisateurId, new List<Guid>(), Genre.Action, budget);
        
        _realisateurRepositoryMock.Setup(r => r.RealisateurExistAsync(realisateurId, CancellationToken.None))
            .ReturnsAsync(true); 
        
        var exception = await Assert.ThrowsAsync<UnproccessableEntityException>(async () => await _createFilmCommandHandlerMock.Handle(command, CancellationToken.None));
        Assert.Equal("L'année de sortie du film est invalide.", exception.Message);
    }
    
    [Theory]
    [InlineData("kill bill", 2024, 200000,  "29db4635-020e-4c0a-8075-91d276fe719d")]
    public async Task ShouldReturnException_WhenRealisateurInvalid(string titre, int annee, int budget, Guid realisateurId)
    {
        var command = new CreateFilmCommand(titre, annee, realisateurId, new List<Guid>(), Genre.Action, budget);
        
        _realisateurRepositoryMock.Setup(r => r.RealisateurExistAsync(realisateurId, CancellationToken.None))
            .ReturnsAsync(false); 
        
        var exception = await Assert.ThrowsAsync<UnproccessableEntityException>(async () => await _createFilmCommandHandlerMock.Handle(command, CancellationToken.None));
        Assert.Equal("Le réalisateur n'est pas présent dans la base de données.", exception.Message);
    }
}