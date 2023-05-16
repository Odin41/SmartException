using Api.V1.Users.Models;
using AutoMapper;
using Entities.Models;

namespace Api.V1.Users.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}