using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Lms.SDK.Enums;
using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lms.Api.Db.Models;

public class LessonAnswer : Entity, ILessonAnswer<Lesson, Course, LessonField>
{
    public LessonAnswerStatus Status { get; set; }
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public required long AuthorId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastStatusDate { get; set; } = DateTimeOffset.UtcNow;
    public long? CheckerId { get; set; }

    [Column(TypeName = "jsonb")]
    public ExpandoObject Data { get; set; } = new ExpandoObject();
}
