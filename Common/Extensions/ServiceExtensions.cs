using Api.OpenApi;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwaggerMinimalApi(this IServiceCollection services)
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

    public static void ConfigureSwaggerUiMinimalApi(this WebApplication app)
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
    
}