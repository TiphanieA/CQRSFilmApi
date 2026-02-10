using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Shared.Enums;

namespace CQRS.Domain.Entities;

public class Film
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get;  set; }
    
    public virtual string Titre { get; private set; }
    
    public virtual int Annee { get;  private set; }

    public virtual Guid RealisateurId { get; private set; }
    
    public virtual Realisateur Realisateur { get;  set; }
    
    public virtual int Budget { get; private set; }
    
    [Column(TypeName = "TINYINT")]
    public virtual Genre Genre { get;private set; }
    
    private readonly List<Acteur> _acteurs = new();

    public virtual IReadOnlyCollection<Acteur> Acteurs => _acteurs.AsReadOnly();

    public Film(string titre, int annee, Guid realisateurId, int budget, Genre genre)
    {
        Titre = titre;
        Annee = annee;
        RealisateurId = realisateurId;
        Budget = budget;
        Genre = genre;
    }

    public void AddActeurs(List<Acteur> acteurs)
    {
        _acteurs.AddRange(acteurs);
    }
}