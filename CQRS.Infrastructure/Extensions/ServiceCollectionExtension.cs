using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CQRS.Application.Interfaces;
using CQRS.Infrastructure.Repositories;

namespace CQRS.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FilmDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("CQRS.Infrastructure.Migrations")));
        
        services.AddTransient<IFilmRepository, FilmRepository>();
        services.AddTransient<IActeurRepository, ActeurRepository>();
        services.AddTransient<IRealisateurRepository, RealisateurRepository>();
        
        return services;
    }
}