using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SmartException.Api.v1.Users.Models;
using SmartException.Data;

namespace SmartException.Api.v1.Users;

public static class UserEndpoints
{
    public static void RegisterUserV1(this WebApplication app)
    {
        var userGroup = app.MapGroup("/users");
        
        userGroup.MapGet("/all", GetAllUsers).WithOpenApi();
        userGroup.MapPost("/add", Create).WithOpenApi();
        userGroup.MapDelete("/delete", Delete).WithOpenApi();
    }

    private static ActionResult<IList<UserDto>> GetAllUsers(ApplicationDbContext context, IMapper mapper )
    {
        var users = mapper.ProjectTo<UserDto>(context.Users);
        
        return new JsonResult(users.ToList());
    }
    
    private static ActionResult<bool> Create(UserDto user, ApplicationDbContext context, IMapper mapper)
    {
        user.Email = user.Email.Trim().ToLower();
        
        var userEntity = mapper.Map<User>(user);
        context.Users.Add(userEntity);
        context.SaveChanges();
        
        return true;
    }
    
    private static ActionResult<bool> Delete(string email, ApplicationDbContext context)
    {
        email = email.Trim().ToLower();
        
        var existsUser = context.Users.FirstOrDefault(u=>u.Email.ToLower() == email);
        if (existsUser == null)
        {
            throw new KeyNotFoundException($"Пользователь с Email {email} не найден.");
        }

        context.Users.Remove(existsUser);
        context.SaveChanges();
        
        return new JsonResult(true);
    }
}