using Api.Extensions;
using Api.V1.Users;
using Api.V2.Users;
using Common.Middleware;
using Asp.Versioning.Conventions;
using Common.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureMappings();
builder.Services.ConfigureServices();
builder.Services.ConfigureSwaggerMinimalApi();

builder.Services.ConfigureApiServices();

var app = builder.Build();

app.MapEndpointConfigure();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.ConfigureSwaggerUiMinimalApi();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.Run();