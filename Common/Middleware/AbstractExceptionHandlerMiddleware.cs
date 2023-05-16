using System.Net;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Common.Middleware;

/// <summary>
/// Abstract handler for all exceptions.
/// </summary>
public abstract class AbstractExceptionHandlerMiddleware
{
    // Enrich is a custom extension method that enriches the Serilog functionality - you may ignore it
    private readonly ILoggerManager _logger;

    private readonly RequestDelegate _next;

    /// <summary>
    /// Gets HTTP status code response and message to be returned to the caller.
    /// Use the ".Data" property to set the key of the messages if it's localized.
    /// </summary>
    /// <param name="exception">The actual exception</param>
    /// <returns>Tuple of HTTP status code and a message</returns>
    public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

    public AbstractExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            // log the error
            _logger.LogError($"{exception.Message} error during executing  {context.Request.Path.Value}");
            var response = context.Response;
            response.ContentType = "application/json";
            
            // get the response code and message
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}