using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.LessonAnswerDto;

public class LessonAnswerListItemResponse : IResponse
{
    public long Id { get; set; }
    public LessonAnswerStatus Status { get; set; }
    public long LessonId { get; set; }
    public required long AuthorId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastStatusDate { get; set; }
    public long? CheckerId { get; set; }
}
