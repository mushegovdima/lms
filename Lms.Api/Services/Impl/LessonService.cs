using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lms.Api.Db;
using Lms.Api.Db.Models;
using Lms.Api.Dto.LessonDto;
using Lms.SDK.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Services.Impl;

internal class LessonService : EntityServiceBase<Lesson>, ILessonService
{
    private readonly ICourseRoleService _courseRoleService;
    public LessonService(
        DataContext db,
        IMapper mapper,
        IHttpContextAccessor accessor,
        ICourseRoleService courseRoleService) : base(db, mapper, accessor)
    {
        _courseRoleService = courseRoleService;
    }

    public override IQueryable<Lesson> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Fields)
            .OrderBy(x => x.Position);
    }

    public async Task<Lesson> Update(LessonPutRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await Load(request.Id, true, cancellationToken);
        Db.Entry(entity).CurrentValues.SetValues(request);
        entity.Fields = request.Fields;
        return await Update(entity, cancellationToken);
    }

    public async Task<IEnumerable<TResponse>> GetByCourse<TResponse>(long courseId, CancellationToken cancellationToken = default)
    {
        var visibleHidden = await _courseRoleService.UserHasRoles(courseId, Role.Admin);
        return await GetQuery()
            .Where(x => x.CourseId == courseId && (visibleHidden || x.Status == LessonStatus.Active))
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}
