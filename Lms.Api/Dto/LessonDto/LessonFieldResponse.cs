using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.LessonDto;

public class LessonFieldResponse : IResponse
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public bool Required { get; set; }
    public FieldType Type { get; set; }
    public uint Position { get; set; }
    public long LessonId { get; set; }
    public required string Title { get; set; }
}
