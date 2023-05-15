using AutoMapper;
using Entities.Models;
using SmartException.V1.Users.Models;

namespace DAL.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}