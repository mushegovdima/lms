using Lms.SDK.Common;

namespace Lms.SDK.Interfaces;

/// <summary>
/// Course
/// </summary>
/// <typeparam name="TLesson"></typeparam>
/// <typeparam name="TCourse"></typeparam>
/// <typeparam name="TCabinet"></typeparam>
/// <typeparam name="TLessonField"></typeparam>
public interface ICourse<TLesson, TCourse, TCabinet, TLessonField> : IEntity, IHasTitle
    where TCourse : ICourse<TLesson, TCourse, TCabinet, TLessonField>
    where TLesson : ILesson<TLesson, TCourse, TCabinet, TLessonField>
    where TCabinet: ICabinet
{
    /// <summary>
    /// Cabinet id
    /// </summary>
    public long CabinetId { get; set; }

    /// <summary>
    /// Cabinet
    /// </summary>
    public TCabinet Cabinet { get; set; }

    /// <summary>
    /// Lessons
    /// </summary>
    public ICollection<TLesson> Lessons { get; set; }
}