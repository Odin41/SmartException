using AutoMapper;
using Contracts;
using DAL;
using Entities.Models;
using SmartException.V1.Users.Models;

namespace Services;

public class UserService: IUsersService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IList<UserDto> GetAllUsers()
    {
        var users = _mapper.ProjectTo<UserDto>(_context.Users);
        
        return users.ToList();
    }

    public UserDto GetUser(string guid)
    {
        var user = _context.Users.FirstOrDefault(u => u.Guid.ToString() == guid.Trim());
        if (user == null)
        {
            throw new KeyNotFoundException($"Пользователь с Guid {guid} не найден.");
        }

        var dto = _mapper.Map<UserDto>(user);

        return dto;
    }

    public bool DeleteUser(string guid)
    {
        var existsUser = _context.Users.FirstOrDefault(u=>u.Guid.ToString() == guid.Trim());
        if (existsUser == null)
        {
            throw new KeyNotFoundException($"Пользователь с Guid {guid} не найден.");
        }

        _context.Users.Remove(existsUser);
        _context.SaveChanges();

        return true;
    }

    public bool AddUser(UserDto user)
    {
        user.Email = user.Email.Trim().ToLower();
        user.Guid = Guid.NewGuid();
        
        var userEntity = _mapper.Map<User>(user);
        _context.Users.Add(userEntity);
        _context.SaveChanges();

        return true;
    }
}