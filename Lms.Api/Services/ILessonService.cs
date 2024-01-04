using Lms.Api.Db.Models;
using Lms.SDK.Services;

namespace Lms.Api.Services;

/// <summary>
/// Lesson service
/// </summary>
public interface ILessonService : IEntityService<Lesson>
{
    /// <summary>
    /// Get lessons by course
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="courseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TResponse>> GetByCourse<TResponse>(long courseId, CancellationToken cancellationToken);
}
