using Api.Extensions;
using Api.V1;
using Api.V2;
using Common.Middleware;
using DAL.Extensions;
using Asp.Versioning.Conventions;
using Common.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureMappings();
builder.Services.ConfigureServices();
builder.Services.ConfigureSwaggerMinimalApi();

var app = builder.Build();

var users = app.NewApiVersionSet()
    .HasApiVersion(1,0)
    .HasApiVersion(2,0)
    .ReportApiVersions()
    .Build();

app.UsersRegisterV1(users);
app.UsersRegisterV2(users);

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