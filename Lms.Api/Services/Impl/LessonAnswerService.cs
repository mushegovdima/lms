using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lms.Api.Abstract;
using Lms.Api.Db;
using Lms.Api.Db.Models;
using Lms.SDK.Common;
using Lms.SDK.Enums;
using Lms.SDK.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace Lms.Api.Services.Impl;

internal class LessonAnswerService : EntityServiceBase<LessonAnswer>, ILessonAnswerService
{
    private readonly ICourseRoleService _courseRoleService;
    public LessonAnswerService(
        DataContext db,
        IMapper mapper,
        IHttpContextAccessor accessor,
        ICourseRoleService courseRoleService) : base(db, mapper, accessor)
    {
        _courseRoleService = courseRoleService;
    }

    public override IQueryable<LessonAnswer> GetQuery()
    {
        return base.GetQuery().Include(x => x.Lesson);
    }

    public async Task<IFilterResponse<TResponse>> GetByFilter<TResponse>(BaseFilter<LessonAnswer, TResponse> filter, CancellationToken cancellationToken = default)
        where TResponse : IResponse
    {
        return await filter.GetResponse(GetQuery(), Mapper, cancellationToken);
    }

    public async Task<TResponse?> GetByLessonId<TResponse>(long lessonId, long authorId, CancellationToken cancellationToken) where TResponse : IResponse
    {
        return await GetQuery()
            .Where(x => x.LessonId == lessonId && x.AuthorId == authorId)
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public override Task<long> Create(LessonAnswer entity, CancellationToken cancellationToken = default)
    {
        entity.AuthorId = User.UserId();
        return base.Create(entity, cancellationToken);
    }

    #region business process

    public async Task SendToCheck(long id, CancellationToken cancellationToken = default)
    {
        await ChangeStatus(id, LessonAnswerStatus.Send, cancellationToken);
    }

    public async Task Approve(long id, CancellationToken cancellationToken = default)
    {
        await ChangeStatus(id, LessonAnswerStatus.Successfull, cancellationToken);
    }

    public async Task Cancel(long id, CancellationToken cancellationToken = default)
    {
        await ChangeStatus(id, LessonAnswerStatus.Cancelled, cancellationToken);
    }

    /// <summary>
    /// Rules for changing statuses
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private async Task<bool> CanChangeStatus(LessonAnswer entity,  LessonAnswerStatus status)
    {
        if (status == entity.Status) return false;

        if (User.IsAdmin()) return true;
        return entity.Status switch
        {
            LessonAnswerStatus.Draft when status == LessonAnswerStatus.Send =>
                await _courseRoleService.UserHasRoles(entity.Lesson.CourseId, Role.Admin, Role.Student),
            LessonAnswerStatus.Send when status == LessonAnswerStatus.Successfull =>
                await _courseRoleService.UserHasRoles(entity.Lesson.CourseId, Role.Admin, Role.Checker),
            LessonAnswerStatus.Successfull when status == LessonAnswerStatus.Cancelled =>
                await _courseRoleService.UserHasRoles(entity.Lesson.CourseId, Role.Admin, Role.Checker),
            LessonAnswerStatus.Cancelled when status == LessonAnswerStatus.Send =>
                await _courseRoleService.UserHasRoles(entity.Lesson.CourseId, Role.Admin, Role.Student),
            _ => false
        };
    }

    private async Task ChangeStatus(long id, LessonAnswerStatus newStatus, CancellationToken cancellationToken)
    {
        var entity = await Load(id, true, cancellationToken);
        if (!await CanChangeStatus(entity, newStatus))
            throw new InvalidOperationException($"Can`t change status from {entity.Status.GetDisplayName()} to {newStatus.GetDisplayName()}");

        entity.LastStatusDate = DateTimeOffset.UtcNow;
        entity.Status = newStatus;
        await Update(entity, cancellationToken);
    }

    #endregion
}

