using Lms.SDK.Common;

namespace Lms.SDK.Interfaces;

/// <summary>
/// Course
/// </summary>
/// <typeparam name="TLesson"></typeparam>
/// <typeparam name="TCourse"></typeparam>
/// <typeparam name="TLessonField"></typeparam>
public interface ICourse<TLesson, TCourse, TLessonField> : IEntity, IHasTitle
    where TCourse : ICourse<TLesson, TCourse, TLessonField>
    where TLesson : ILesson<TLesson, TCourse, TLessonField>
{
    /// <summary>
    /// Desc
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Author
    /// </summary>
    public long AuthorId { get; set; }

    /// <summary>
    /// Lessons
    /// </summary>
    public ICollection<TLesson> Lessons { get; set; }
}