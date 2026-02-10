using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRS.Domain.Entities;

public class IntervenantBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; private set; }
    
    public virtual string Nom { get; private set; }
    
    public virtual string Prenom { get; private set; }

    public IntervenantBase(string nom, string prenom)
    {
        Nom = nom;
        Prenom = prenom;
    }
}