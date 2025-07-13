using Blockbuster.API;
using Blockbuster.Infrastructure.Configuration;
using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.AcceptanceTests.Hooks
{
    [Binding]
    public sealed class TestHooks
    {
        private readonly TestContext _testContext;


        public TestHooks(TestContext testContext)
        {
            _testContext = testContext;
        }

        [BeforeScenario]
        public void BeforeScenarioAsync()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            var webApplicationFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    IConfigurationSection? configSection = null;
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile("appsettings.json");
                        configSection = context.Configuration.GetSection("BlockbusterConfig");
                    });
                    builder.ConfigureTestServices(services =>
                        services.Configure<BlockbusterConfig>(configSection));
                });

            _testContext.ApplicationFactory = webApplicationFactory;



        }

    }
}
