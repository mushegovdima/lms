using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.SDK.Interfaces;

/// <summary>
/// Lesson answer
/// </summary>
/// <typeparam name="TLesson"></typeparam>
/// <typeparam name="TCourse"></typeparam>
/// <typeparam name="TCabinet"></typeparam>
/// <typeparam name="TLessonField"></typeparam>
public interface ILessonAnswer<TLesson, TCourse, TCabinet, TLessonField> : IDynamicEntity
    where TCabinet : ICabinet
    where TCourse : ICourse<TLesson, TCourse, TCabinet, TLessonField>
    where TLesson : ILesson<TLesson, TCourse, TCabinet, TLessonField>
{
    /// <summary>
    /// Status
    /// </summary>
    public LessonAnswerStatus Status { get; set; }

    /// <summary>
    /// Lessong Id
    /// </summary>
    public long LessonId { get; set; }

    /// <summary>
    /// Lesson
    /// </summary>
    public TLesson Lesson { get; set; }

    /// <summary>
    /// Author
    /// </summary>
    public long AuthorId { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Last status date
    /// </summary>
    public DateTimeOffset LastStatusDate { get; set; }

    /// <summary>
    /// Checker id
    /// </summary>
    public long? CheckerId { get; set; }
}
