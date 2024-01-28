using Lms.Api.Db.Models;
using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.LessonDto;

public class LessonPutRequest : IPutRequest
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public required string Title { get; set; }
    public LessonStatus Status { get; set; }
    public required ICollection<LessonField> Fields { get; set; }
}
