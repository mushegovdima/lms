using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Lms.SDK.Enums;
using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lms.Api.Db.Models;

public class LessonAnswer : Entity, ILessonAnswer<Lesson, Course, Cabinet, LessonField>
{
    public LessonAnswerStatus Status { get; set; }
    public long LessonId { get; set; }
    public required Lesson Lesson { get; set; }
    public required long AuthorId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastStatusDate { get; set; } = DateTimeOffset.Now;
    public long? CheckerId { get; set; }
    public User? Checker { get; set; }

    [Column(TypeName = "jsonb")]
    public ExpandoObject Data { get; set; } = new ExpandoObject();
}
