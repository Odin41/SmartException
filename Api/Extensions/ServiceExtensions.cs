using Api.OpenApi;
using Asp.Versioning;
using Common.Interfaces;
using Contracts;
using DAL;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Extensions;

public static class ServiceExtensions
{

    public static void ConfigureLoggerService(this IServiceCollection service)
    {
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        service.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureDbContext(this IServiceCollection service, IConfiguration config)
    {
        // Add services to the container.
        var connectionString = config.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UserService>();
       
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(opt => opt.OperationFilter<SwaggerDefaultValues>());

        services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1,0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
    }

    public static void ConfigureSwaggerUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                opt.SwaggerEndpoint(url, name);
            }
        });
    }


    /*private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"Api version {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += $"{Environment.NewLine}This API version has been deprecated.";
        }

        return info;
    }
*/

}