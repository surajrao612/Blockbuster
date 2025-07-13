using Blockbuster.Infrastructure.Configuration;
using Blockbuster.Infrastructure.Interfaces;
using Blockbuster.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddMemoryCache();


        var blockbusterConfig = config.GetSection("BlockbusterConfig").Get<BlockbusterConfig>();


        //services.Configure<BlockbusterConfig>(config.GetSection("BlockbusterConfig"));


        services.BindOptions<BlockbusterConfig>("BlockbusterConfig").AddHttpClient<ICinemaWorldService, CinemaWorldService>(client =>
        {
            client.BaseAddress = new Uri(blockbusterConfig.CinemaWorldServiceUrl);
        })
            .AddPolicyHandler((sp, _) =>
            {
                var logger = sp.GetService<ILogger<CinemaWorldService>>();
                return CreateRetryPolicy(logger, blockbusterConfig.RetryPolicySettings);
            });


        services.BindOptions<BlockbusterConfig>("BlockbusterConfig").AddHttpClient<IFilmWorldService, FilmWorldService>(client =>
        {
            client.BaseAddress = new Uri(blockbusterConfig?.FilmWorldServiceUrl);
        })
            .AddPolicyHandler((sp, _) =>
            {
                var logger = sp.GetService<ILogger<FilmWorldService>>();
                return CreateRetryPolicy(logger, blockbusterConfig.RetryPolicySettings);
            });


        return services;
    }


    private static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(ILogger logger, RetryPolicySettings policySettings)
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(policySettings.RetryCount, _ => TimeSpan.FromSeconds(policySettings.DelaySeconds), onRetry: (clientException, retryCount) =>
            {
                logger.LogWarning($"Retrying request. Retry count: {retryCount}");
            });

        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

        return Policy.WrapAsync(retryPolicy, timeoutPolicy);
    }

    public static IServiceCollection BindOptions<TOptions>(this IServiceCollection @this, string configSectionPath)
        where TOptions : class =>
        @this.AddOptions<TOptions>().BindConfiguration(configSectionPath).Services;
}
