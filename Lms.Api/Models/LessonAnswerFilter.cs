using Lms.Api.Abstract;
using Lms.Api.Db.Models;
using Lms.Api.Dto.LessonAnswerDto;
using Lms.SDK.Enums;

namespace Lms.Api.Models;

public class LessonAnswerFilter : BaseFilter<LessonAnswer, LessonAnswerListItemResponse>
{
    public LessonAnswerStatus? Status { get; set; }
    public long? LessonId { get; set; }
    public long? AuthorId { get; set; }
    public long? CheckerId { get; set; }

    protected override IQueryable<LessonAnswer> ApplyFilter(IQueryable<LessonAnswer> query)
    {
        if (LessonId.HasValue)
            query = query.Where(x => x.LessonId == LessonId);

        if (Status.HasValue)
            query = query.Where(x => x.Status == Status);

        if (AuthorId.HasValue)
            query = query.Where(x => x.AuthorId == AuthorId);

        if (CheckerId.HasValue)
            query = query.Where(x => x.CheckerId == CheckerId);

        return query;
    }
}
