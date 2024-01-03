using System.Security.Claims;
using Lms.Api.Db.Models;
using Lms.SDK.Enums;
using Lms.SDK.Services;

namespace Lms.Api.Services;

/// <summary>
/// Service of course roles
/// </summary>
public interface ICourseRoleService: IEntityService<CourseRole>
{
    public Task<bool> UserHasRoles(long courseId, params Role[] roles);

    public Task<IEnumerable<TResponse>> GetByCourse<TResponse>(long courseId, CancellationToken cancellationToken);

    public Task<long> AddUserRole(long userId, long courseId, Role role, CancellationToken cancellationToken);

    public Task RemoveUserRole(long id, CancellationToken cancellationToken);
}
