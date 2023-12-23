using System.ComponentModel.DataAnnotations;
using Lms.SDK.Interfaces;

namespace Lms.Api.Db.Models;

public class Course : Entity, ICourse<Lesson, Course, LessonField>
{
    [StringLength(250)]
    public required string Title { get; set; }

    public string Description { get; set; } = string.Empty;

    public required long CabinetId { get; set; }

    public required long AuthorId { get; set; }

    public required ICollection<Lesson> Lessons { get; set; }

}
