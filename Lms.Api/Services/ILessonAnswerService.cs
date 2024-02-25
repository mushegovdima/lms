using Lms.Api.Abstract;
using Lms.Api.Db.Models;
using Lms.SDK.Common;
using Lms.SDK.Services;

namespace Lms.Api.Services;

/// <summary>
/// Lesson answer service
/// </summary>
public interface ILessonAnswerService : IEntityService<LessonAnswer>
{
    /// <summary>
    /// get by filter
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IFilterResponse<TResponse>> GetByFilter<TResponse>(BaseFilter<LessonAnswer,TResponse> filter, CancellationToken cancellationToken)
        where TResponse : IResponse;


    /// <summary>
    /// Get by lesson id
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="lessonId"></param>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TResponse?> GetByLessonId<TResponse>(long lessonId, long authorId, CancellationToken cancellationToken)
        where TResponse : IResponse;

    /// <summary>
    /// Send to check
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SendToCheck(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Approve answer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Approve(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Cancel answer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Cancel(long id, CancellationToken cancellationToken);
}
