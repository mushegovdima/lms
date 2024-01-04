using Lms.Api.Db.Models;
using Lms.SDK.Enums;
using Lms.SDK.Services;

namespace Lms.Api.Services;

/// <summary>
/// Service of course roles
/// </summary>
public interface ICourseRoleService: IEntityService<CourseRole>
{
    /// <summary>
    /// User has roles
    /// </summary>
    /// <param name="courseId"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public Task<bool> UserHasRoles(long courseId, params Role[] roles);

    /// <summary>
    /// Get by course
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="courseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TResponse>> GetByCourse<TResponse>(long courseId, CancellationToken cancellationToken);

    /// <summary>
    /// Add role to course
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="courseId"></param>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<long> AddUserRole(long userId, long courseId, Role role, CancellationToken cancellationToken);

    /// <summary>
    /// Remove userRole
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task RemoveUserRole(long id, CancellationToken cancellationToken);
}
