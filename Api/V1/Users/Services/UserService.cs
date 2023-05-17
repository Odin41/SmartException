using Api.V1.Users.Interfaces;
using Api.V1.Users.Models;
using AutoMapper;
using Common.Exceptions;
using DAL;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.V1.Users.Services;

public class UserService : IUsersService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = _mapper.ProjectTo<UserDto>(_context.Users);
        
        return await users.ToArrayAsync();
    }

    public async Task<UserDto> GetUserAsync(string guid)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid.ToString() == guid.Trim());
        if (user == null)
        {
            throw new NotFoundException($"Пользователь с Guid {guid} не найден.");
        }

        var dto = _mapper.Map<UserDto>(user);

        return dto;
    }

    public async Task<bool> DeleteUserAsync(string guid)
    {
        var existsUser = await _context.Users.FirstOrDefaultAsync(u=>u.Guid.ToString() == guid.Trim());
        if (existsUser == null)
        {
            throw new NotFoundException($"Пользователь с Guid {guid} не найден.");
        }

        _context.Users.Remove(existsUser);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddUserAsync(UserDto user)
    {
        user.Email = user.Email.Trim().ToLower();
        user.Guid = Guid.NewGuid();
        
        var userEntity = _mapper.Map<User>(user);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(UserDto user)
    {
        var existsUser = await _context.Users.FirstOrDefaultAsync(u=>u.Guid.ToString().ToLower() == user.Guid.ToString().Trim().ToLower());
        if (existsUser == null)
        {
            throw new NotFoundException($"Пользователь с Guid {user.Guid} не найден.");
        }

        _mapper.Map(user, existsUser);
        await _context.SaveChangesAsync();
        
        return true;
    }
}