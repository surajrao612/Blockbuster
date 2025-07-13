
using Blockbuster.Application.DependencyInjection;
using Blockbuster.Infrastructure.Configuration;
using Blockbuster.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;
using System.Threading.RateLimiting;

namespace Blockbuster.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.Configure<BlockbusterConfig>(builder.Configuration.GetSection("BlockbusterConfig"));
        builder.Services.Configure<APIRateLimiterConfig>(builder.Configuration.GetSection("APIRateLimiterConfig"));

        // Add services to the container.


        builder.Services.AddApplicationServices()
            .AddInfrastructureServices(builder.Configuration);


        builder.Services.AddControllers();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        

        var rateLimitConfig = builder.Configuration.GetSection("APIRateLimiterConfig").Get<APIRateLimiterConfig>();

        builder.Services.AddRateLimiter(options =>
        {
            
            options.AddSlidingWindowLimiter("Sliding", opt =>
            {
                opt.Window = TimeSpan.FromMinutes(1);
                opt.PermitLimit = rateLimitConfig.AllowedRequestsPerMinute;
                opt.QueueLimit = rateLimitConfig.QueueLimit;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.SegmentsPerWindow = 2;
            });

            options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;

        });

        builder.Services.AddCors(options => options.AddPolicy("Default", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors("Default");


        app.UseHttpsRedirection();

        app.UseRateLimiter();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

