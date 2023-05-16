using Api.V2.Users.Interfaces;
using Api.V2.Users.Models;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Api.V2.Users;
public static class UsersApi
{
    public static void UsersRegisterV2(this WebApplication app,  ApiVersionSet apiSet)
    {
        app.MapGet("/v{api-version:apiVersion}/all", GetAllUsers)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);

        app.MapPost("/v{api-version:apiVersion}/add", Create)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);
        
        app.MapDelete("/v{api-version:apiVersion}/delete", Delete)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);
        
        app.MapGet("/v{api-version:apiVersion}/{guid}", GetUser)
            .WithApiVersionSet(apiSet)
            .MapToApiVersion(2);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    private static async Task<IEnumerable<UserDto>> GetAllUsers(IUsersService usersService)
    {
        var users = await usersService.GetAllUsersAsync();
        
        return users.ToList();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    private static async Task<ActionResult<UserDto>> GetUser(string guid, IUsersService usersService)
    {
        return await usersService.GetUserAsync(guid);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    private static async Task<ActionResult<bool>> Create(UserDto user, IUsersService usersService)
    {
        return await usersService.AddUserAsync(user);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    private static async Task<ActionResult<bool>> Delete(string guid, IUsersService usersService)
    {
        return await usersService.DeleteUserAsync(guid);
    }
}