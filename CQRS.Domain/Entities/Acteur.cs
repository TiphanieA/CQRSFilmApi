namespace CQRS.Domain.Entities;

public class Acteur : IntervenantBase
{
    public Acteur(string nom, string prenom) : base(nom, prenom)
    {
    }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}