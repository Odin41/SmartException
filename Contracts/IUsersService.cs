using SmartException.V1.Users.Models;

namespace Contracts;

public interface IUsersService
{
    public IList<UserDto> GetAllUsers();

    public UserDto GetUser(string guid);

    public bool DeleteUser(string guid);

    public bool AddUser(UserDto user);
}