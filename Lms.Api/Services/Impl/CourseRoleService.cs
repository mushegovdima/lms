using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lms.Api.Db;
using Lms.Api.Db.Models;
using Lms.Api.Dto.CourseDto;
using Lms.SDK.Enums;
using Lms.SDK.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Services.Impl;

public class CourseRoleService : EntityServiceBase<CourseRole>, ICourseRoleService
{
    public CourseRoleService(DataContext db, IMapper mapper, IHttpContextAccessor accessor) : base(db, mapper, accessor)
    {
    }

    public Task<long> AddUserRole(long userId, long courseId, Role role, CancellationToken cancellationToken = default)
    {
        return Create(new CourseRolePostRequest
            {
                UserId = userId,
                CourseId = courseId,
                Role = role
            }, 
            cancellationToken);
    }

    public async Task RemoveUserRole(long id, CancellationToken cancellationToken = default)
    {
        var entity = await Load(id, true, cancellationToken);
        if (entity is null) throw new KeyNotFoundException();

        await Delete(entity, cancellationToken);
    }

    public override Task<CourseRole> Update(CourseRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Course?.AuthorId == entity.UserId)
            throw new ArgumentException("Can't update author rules");

        return base.Update(entity, cancellationToken);
    }

    public override Task Delete(CourseRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Course?.AuthorId == entity.UserId)
            throw new ArgumentException("Can't update author rules");

        return base.Delete(entity, cancellationToken);
    }

    public async Task<IEnumerable<TResponse>> GetByCourse<TResponse>(long courseId, CancellationToken cancellationToken)
    {
        return await GetQuery()
            .Where(x => x.CourseId == courseId)
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<bool> UserHasRoles(long courseId, params Role[] roles)
    {
        if (User.IsAdmin()) return true;
        return await GetQuery().AnyAsync(x => x.CourseId == courseId && roles.Contains(x.Role));
    }
}
