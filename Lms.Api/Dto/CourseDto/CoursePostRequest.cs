using Lms.SDK.Common;

namespace Lms.Api.Dto.CourseDto;

public class CoursePostRequest : IPostRequest
{
    public required string Title { get; set; }
    public required long AuthorId { get; set; }
    public string? Description { get; set; }
}
