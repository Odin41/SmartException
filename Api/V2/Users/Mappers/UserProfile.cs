using Api.V2.Users.Models;
using AutoMapper;
using Entities.Models;

namespace Api.V2.Users.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}