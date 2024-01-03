using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;

namespace Lms.Api.Services.Impl
{
    public class LessonAnswerService : EntityServiceBase<LessonAnswer>
    {
        public LessonAnswerService(DataContext db, IMapper mapper, IHttpContextAccessor accessor) : base(db, mapper, accessor)
        {
        }
    }
}

