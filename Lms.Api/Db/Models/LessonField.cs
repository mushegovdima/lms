using Lms.SDK.Enums;
using Lms.SDK.Interfaces;

namespace Lms.Api.Db.Models;

public class LessonField : Entity, ILessonField
{
    public string? Description { get; set; }
    public bool Required { get; set; }
    public FieldType Type { get; set; }
    public uint Position { get; set; }
    public required string Title { get; set; }
}

