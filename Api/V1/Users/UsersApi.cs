using Api.V1.Users.Interfaces;
using Asp.Versioning.Builder;
using Api.V1.Users.Models;
using Common.Exceptions;

namespace Api.V1.Users;
public static class UsersApi
{
    public static void UsersRegisterV1(this WebApplication app, ApiVersionSet apiSet)
    {
        app.MapGet("v{api-version:apiVersion}/users", async (IUsersService usersService) =>
            {
                var users = await usersService.GetAllUsersAsync();

                return Results.Ok(users.ToArray());
            })
            .Produces<IEnumerable<UserDto>>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(1);

        app.MapPost("v{api-version:apiVersion}/users", async (UserDto user, IUsersService usersService) =>
                await usersService.AddUserAsync(user))
            .Produces<bool>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(1);

        app.MapDelete("v{api-version:apiVersion}/users/{guid}", async (string guid, IUsersService usersService) =>
                await usersService.DeleteUserAsync(guid))
            .Produces<bool>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(1);

        app.MapGet("v{api-version:apiVersion}/users/{guid}", async (string guid, IUsersService usersService) =>
                await usersService.GetUserAsync(guid))
            .Produces<UserDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(1);

        app.MapPut("v{api-version:apiVersion}/users", async (UserDto user, IUsersService usersService) =>
                await usersService.UpdateAsync(user))
            .Produces<bool>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(1);
    }
}