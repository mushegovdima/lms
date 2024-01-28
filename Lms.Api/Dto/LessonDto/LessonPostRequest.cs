using Lms.SDK.Common;

namespace Lms.Api.Dto.LessonDto;

public class LessonPostRequest : IPostRequest
{
    public string? Description { get; set; }
    public string? Image { get; set; }
    public required long CourseId { get; set; }
    public required string Title { get; set; }
}
