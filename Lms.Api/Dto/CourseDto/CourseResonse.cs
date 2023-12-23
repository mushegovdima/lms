using Lms.SDK.Common;

namespace Lms.Api.Dto.CourseDto;

public class CourseResponse : IResponse
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required long AuthorId { get; set; }
    public required int LessonsCount { get; set; }
}
