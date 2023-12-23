using System.ComponentModel.DataAnnotations;
using Lms.SDK.Enums;
using Lms.SDK.Common;

namespace Lms.SDK.Interfaces;

/// <summary>
/// Lesson
/// </summary>
/// <typeparam name="TLesson"></typeparam>
/// <typeparam name="TCourse"></typeparam>
/// <typeparam name="TLessonField"></typeparam>
public interface ILesson<TLesson, TCourse, TLessonField> : IEntity, IHasTitle
    where TLesson : ILesson<TLesson, TCourse, TLessonField>
    where TCourse: ICourse<TLesson, TCourse, TLessonField>
{
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public LessonStatus Status { get; set; }

    public ICollection<TLessonField> Fields { get; set; }

    /// <summary>
    /// Lesson avatar
    /// </summary>
    [DataType(DataType.ImageUrl)]
    public string? Image { get; set; }

    /// <summary>
    /// CabinetId
    /// </summary>
    public long CabinetId { get; set; }

    /// <summary>
    /// Course id
    /// </summary>
    public long CourseId { get; set; }

    /// <summary>
    /// Course
    /// </summary>
    public TCourse Course { get; set; }
}
