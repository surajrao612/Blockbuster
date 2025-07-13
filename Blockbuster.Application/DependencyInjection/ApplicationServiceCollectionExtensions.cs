using Blockbuster.Application.Interfaces;
using Blockbuster.Application.Mapper;
using Blockbuster.Application.Movies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
