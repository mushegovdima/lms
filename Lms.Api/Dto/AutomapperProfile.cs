using AutoMapper;
using Lms.Api.Db.Models;
using Lms.Api.Dto.PostRequest;
using Lms.Api.Dto.PutRequests;
using Lms.Api.Dto.Response;

namespace Lms.Api.Dto;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Cabinet, CabinetResponse>();
        CreateMap<CabinetPutRequest, Cabinet>();
        CreateMap<CabinetPostRequest, Cabinet>();


        CreateMap<User, UserResponse>();
        CreateMap<UserPutRequest, User>();
        CreateMap<UserPostRequest, User>();
    }
}

