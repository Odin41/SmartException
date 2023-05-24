using Api.Extensions;
using Common.Middleware;
using Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureMappings();
builder.Services.ConfigureSwaggerMinimalApi();

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