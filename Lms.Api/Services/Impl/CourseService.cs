using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;

namespace Lms.Api.Services.Impl
{
    public class CourseService : EntityServiceBase<Course>
    {
        public CourseService(DataContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}

