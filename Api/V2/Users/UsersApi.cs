using Api.V2.Users.Interfaces;
using Asp.Versioning.Builder;
using Api.V2.Users.Models;
using Common.Exceptions;

namespace Api.V2.Users;
public static class UsersApi
{
    public static void UsersRegisterV2(this WebApplication app, ApiVersionSet apiSet)
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
            .MapToApiVersion(2);

        app.MapPost("v{api-version:apiVersion}/users", async (UserDto user, IUsersService usersService) =>
                await usersService.AddUserAsync(user))
            .Produces<bool>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);

        app.MapDelete("v{api-version:apiVersion}/users/{guid}", async (string guid, IUsersService usersService) =>
                await usersService.DeleteUserAsync(guid))
            .Produces<bool>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);

        app.MapGet("v{api-version:apiVersion}/users/{guid}", async (string guid, IUsersService usersService) =>
                await usersService.GetUserAsync(guid))
            .Produces<UserDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);
    }
}