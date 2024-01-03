using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;
using Lms.SDK.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Services.Impl
{
    public class CourseService : EntityServiceBase<Course>
    {
        private readonly ICourseRoleService _roleService;
        public CourseService(DataContext db, IMapper mapper, IHttpContextAccessor accessor, ICourseRoleService roleService)
            : base(db, mapper, accessor)
        {
            _roleService = roleService;
        }

        public override IQueryable<Course> GetQuery()
        {
            var user = User.GetUserModel();
            if (user.IsAdmin) return base.GetQuery();

            return base.GetQuery()
                .Include(x => x.AccessRoles)
                .Where(x => x.AccessRoles.Any(x => x.UserId == user.Id))
                .AsSplitQuery();
        }

        public override async Task<long> Create(Course entity, CancellationToken cancellationToken = default)
        {
            var id =  await base.Create(entity, cancellationToken);
            await _roleService.AddUserRole(entity.AuthorId, id, SDK.Enums.Role.Admin, cancellationToken);
            return id;
        }

        public Task<bool> UserCanUpdateCourse(long courseId)
        {
            return _roleService.UserHasRoles(courseId, SDK.Enums.Role.Admin);
        }
    }
}

