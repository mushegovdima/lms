﻿using Lms.SDK.Enums;
using Lms.SDK.Interfaces;

namespace Lms.Api.Db.Models;

public class Lesson : Entity, ILesson<Lesson, Course, Cabinet, LessonField>
{
    public string? Description { get; set; }
    public LessonStatus Status { get; set; }
    public required ICollection<LessonField> Fields { get; set; }
    public string? Image { get; set; }
    public long CabinetId { get; set; }
    public required Cabinet Cabinet { get; set; }
    public long CourseId { get; set; }
    public required Course Course { get; set; }
    public required string Title { get; set; }
}
