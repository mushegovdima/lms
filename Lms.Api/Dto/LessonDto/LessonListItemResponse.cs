using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.LessonDto;

public class LessonListItemResponse : IResponse
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public LessonStatus Status { get; set; }
    public string? Image { get; set; }
    public long CourseId { get; set; }
    public required string Title { get; set; }
    public uint Position { get; set; }

}
