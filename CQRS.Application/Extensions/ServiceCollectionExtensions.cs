using CQRS.Application.Commands;
using CQRS.Application.Interfaces;
using CQRS.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateFilmCommandHandler,CreateFilmCommandHandler>();
        services.AddScoped<IGetFilmByIdQueryHandler,GetFilmByIdQueryHandler>();
        services.AddScoped<IGetFilmsQueryHandler,GetFilmsQueryHandler>();
        
        return services;
    }
}