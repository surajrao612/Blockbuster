using Blockbuster.Application.Interfaces;
using Blockbuster.Application.Mapper;
using Blockbuster.Application.Movies.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blockbuster.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        services.AddScoped<IMovieRetrievalService, MovieRetrievalService>();

        return services;
    }
}
