using CQRS.Shared.Enums;

namespace CQRS.Application.Commands;

public class CreateFilmCommand 
{
    public string Titre { get; private set; }
    public int Annee { get; private set; }
    
    public Guid RealisateurId { get;private set; }
    
    public List<Guid> Acteurs { get;private set; }
    
    public Genre Genre { get; private set; }
    
    public int Budget { get; private set; }

    public CreateFilmCommand(string titre, int annee, Guid realisateurId, List<Guid> acteurs, Genre genre, int budget)
    {
        Titre = titre;
        Annee = annee;
        RealisateurId = realisateurId;
        Acteurs = acteurs;
        Genre = genre;
        Budget = budget;
    }
}