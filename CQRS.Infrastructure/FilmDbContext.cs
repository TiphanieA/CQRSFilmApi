using Microsoft.EntityFrameworkCore;
using CQRS.Domain.Entities;

namespace CQRS.Infrastructure;

public class FilmDbContext: DbContext
{
    public DbSet<Realisateur> Realisateurs { get; set; }
    
    public DbSet<Acteur> Acteurs { get; set; }
    
    public DbSet<Film> Films { get; set; }
    
    public FilmDbContext(DbContextOptions<FilmDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Film>()
            .HasMany(f => f.Acteurs)
            .WithMany(a => a.Films)
            .UsingEntity<Dictionary<string, object>>(
                "FilmActeur", 
                j => j.HasOne<Acteur>().WithMany().HasForeignKey("ActeurId"),
                j => j.HasOne<Film>().WithMany().HasForeignKey("FilmId")
            );
    }
}