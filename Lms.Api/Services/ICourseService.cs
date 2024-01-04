using Lms.Api.Db.Models;
using Lms.SDK.Services;

namespace Lms.Api.Services;

/// <summary>
/// Course service
/// </summary>
public interface ICourseService : IEntityService<Course>
{
    /// <summary>
    /// Check current user can update course
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public Task<bool> UserCanUpdateCourse(long courseId);
}
