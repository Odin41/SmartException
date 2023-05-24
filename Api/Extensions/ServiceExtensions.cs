using Common.Interfaces;
using DAL;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;

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
        services.AddScoped<V1.Users.Interfaces.IUsersService, V1.Users.Services.UserService>();
        services.AddScoped<V2.Users.Interfaces.IUsersService, V2.Users.Services.UserService>();
        
        services.AddTransient<IApi, V2.Users.UsersApi>();
        services.AddTransient<IApi, V1.Users.UsersApi>();
        
    }
}