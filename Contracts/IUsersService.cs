using SmartException.V1.Users.Models;

namespace Contracts;

public interface IUsersService
{
    public Task<IEnumerable<UserDto>> GetAllUsersAsync();

    public Task<UserDto> GetUserAsync(string guid);

    public Task<bool> DeleteUserAsync(string guid);

    public Task<bool> AddUserAsync(UserDto user);
}