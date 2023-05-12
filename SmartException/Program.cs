using SmartException.Api.v1.Users;
using SmartException.Extensions;
using SmartException.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureMappings();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<BackofficeExceptionHandlerMiddleware>();
app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.RegisterUserV1();

app.Run();