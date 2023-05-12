using Entities.Models;
using SmartException.Api.v1.Users.Models;

namespace SmartException.Api.v1.Users.Extensions;

public static class UserDtoMapperExtension
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto()
        {
            Name = user.Name,
            Age = user.Age
        };
    }
}