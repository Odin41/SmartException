using Api.V1.Users.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class MapperConfigurationExtension
{
    public static void ConfigureMappings(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(UserProfile));
    }
}