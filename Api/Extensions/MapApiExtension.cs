using Api.V1.Users;
using Api.V2.Users;
using Asp.Versioning.Conventions;
using Common.Exceptions;
using Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class MapApiExtension
{
    public static void MapEndpointConfigure(this  WebApplication app)
    {
        var apiSet = app.NewApiVersionSet()
            .HasApiVersion(1,0)
            .HasApiVersion(2,0)
            .ReportApiVersions()
            .Build();
            
            var apis = app.Services.GetServices<IApi>();
            foreach(var api in apis)
            {
                if(api is null) 
                {
                    throw new NotFoundException("Api not found");
                }
                
                api.Register(app, apiSet);
            }
    }
}