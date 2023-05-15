using Contracts;
using DAL;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using NLog;
using Services;

namespace SmartException.Extensions;

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
        service.AddDatabaseDeveloperPageExceptionFilter();
    }

    public static void ConfigureServices(this IServiceCollection service)
    {
        service.AddScoped<IUsersService, UserService>();
    }

}