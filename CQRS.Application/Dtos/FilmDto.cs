using CQRS.Shared.Enums;

namespace CQRS.Application.Dtos;

public class FilmDto
{
    public Guid Id { get; set; }
    
    public string Titre { get; set; }
    
    public int Annee { get; set; }
    
    public int Budget { get; set; }
    
    public Guid RealisateurId { get; set; }
    
    public Genre Genre { get; set; }
    
    public List<string> Acteurs { get; set; }

    public FilmDto(Guid id, string titre, int annee, List<string> acteurs, int budget, Guid realisateurId, Genre genre)
    {
        Id = id;
        Titre = titre;
        Annee = annee;
        Budget = budget;
        RealisateurId = realisateurId;
        Genre = genre;
        Acteurs = acteurs;
    }
}