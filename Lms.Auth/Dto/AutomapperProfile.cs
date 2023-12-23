using AutoMapper;
using Lms.Auth.Db.Models;
using Lms.Auth.Dto.CabinetDto;
using Lms.Auth.UserDto;

namespace Lms.Api.Dto;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<UserPutRequest, User>();

        CreateMap<Cabinet, CabinetResponse>();
        CreateMap<CabinetPutRequest, Cabinet>();
        CreateMap<CabinetPostRequest, Cabinet>();
    }
}

