using DAL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Extensions;

public static class MapperConfigurationExtension
{
    public static void ConfigureMappings(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(UserProfile));
    }
}