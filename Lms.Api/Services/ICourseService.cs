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

    /// <summary>
    /// Get by author
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TResponse>> GetByAuthor<TResponse>(long authorId, CancellationToken cancellationToken);

    /// <summary>
    /// Get my courses
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TResponse>> GetMy<TResponse>(CancellationToken cancellationToken);
}
