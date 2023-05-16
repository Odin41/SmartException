using Api.V1.Users;
using Api.V2.Users;
using Asp.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class MapApiExtension
{
    public static void MapEndpointConfigure(this WebApplication app)
    {
        var users = app.NewApiVersionSet()
            .HasApiVersion(1,0)
            .HasApiVersion(2,0)
            .ReportApiVersions()
            .Build();

        app.UsersRegisterV1(users);
        app.UsersRegisterV2(users);
    }
    
    public static void ConfigureApiServices(this IServiceCollection services)
    {
        services.AddScoped<V1.Users.Interfaces.IUsersService, V1.Users.Services.UserService>();
        services.AddScoped<V2.Users.Interfaces.IUsersService, V2.Users.Services.UserService>();
    }
}