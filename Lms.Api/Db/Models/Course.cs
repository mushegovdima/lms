using System.ComponentModel.DataAnnotations;
using Lms.SDK.Interfaces;

namespace Lms.Api.Db.Models;

public class Course : Entity, ICourse<Lesson, Course, Cabinet, LessonField>
{
    [StringLength(250)]
    public required string Title { get; set; }

    public required long CabinetId { get; set; }

    public required Cabinet Cabinet { get; set; }

    public required ICollection<Lesson> Lessons { get; set; }
}
