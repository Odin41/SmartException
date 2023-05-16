using Common.Interfaces;
using Contracts;
using DAL;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Services;

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
}