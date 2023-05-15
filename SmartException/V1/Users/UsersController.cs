using AutoMapper;
using Contracts;
using DAL;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SmartException.V1.Users.Models;

namespace SmartException.V1.Users;

public static class UsersController
{
    public static void RegisterUserV1(this WebApplication app)
    {
        var userGroup = app.MapGroup("/v1/users");
        
        userGroup.MapGet("/all", GetAllUsers).WithOpenApi();
        userGroup.MapPost("/add", Create).WithOpenApi();
        userGroup.MapDelete("/delete", Delete).WithOpenApi();
        userGroup.MapGet("/{guid}", GetUser).WithOpenApi();
    }

    private static IList<UserDto> GetAllUsers(IUsersService usersService)
    {
        var users = usersService.GetAllUsers();
        
        return users.ToList();
    }

    private static ActionResult<UserDto> GetUser(string guid, IUsersService usersService)
    {
        return usersService.GetUser(guid);
    }

    private static ActionResult<bool> Create(UserDto user, IUsersService usersService)
    {
        return usersService.AddUser(user);
    }
    
    private static ActionResult<bool> Delete(string guid, IUsersService usersService)
    {
        return usersService.DeleteUser(guid);
    }
}