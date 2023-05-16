using Api.V2.Users.Models;

namespace Api.V2.Users.Interfaces;

public interface IUsersService
{
    public Task<IEnumerable<UserDto>> GetAllUsersAsync();

    public Task<UserDto> GetUserAsync(string guid);

    public Task<bool> DeleteUserAsync(string guid);

    public Task<bool> AddUserAsync(UserDto user);
}