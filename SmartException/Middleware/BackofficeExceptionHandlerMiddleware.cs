﻿using System.Net;
using Contracts;
using Entities.Models;
using Newtonsoft.Json;
using SmartException.Exceptions;

namespace SmartException.Middleware;

public class BackofficeExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    public BackofficeExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger) : base(next, logger)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        switch (exception)
        {
            case KeyNotFoundException
                or FileNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                break;
            case ValidationException
               or ArgumentException
                or InvalidOperationException:
                code = HttpStatusCode.UnprocessableEntity;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonConvert.SerializeObject(new ResultState(code.ToString(), exception.Message)));
    }
}