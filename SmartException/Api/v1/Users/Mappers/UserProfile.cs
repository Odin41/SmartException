using AutoMapper;
using Entities.Models;
using SmartException.Api.v1.Users.Models;

namespace SmartException.Api.v1.Users.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}