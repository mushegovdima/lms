using AutoMapper;
using Lms.Api.Db.Models;
using Lms.Api.Dto.CourseDto;

namespace Lms.Api.Dto
{

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<Course, CourseResponse>()
                .ForMember(
                    x => x.LessonsCount,
                    o => o.MapFrom(d => d.Lessons.Count()));
        }
    }
}
