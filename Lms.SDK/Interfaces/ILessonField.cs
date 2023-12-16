using Lms.SDK.Enums;
using Lms.SDK.Common;

namespace Lms.SDK.Interfaces;

/// <summary>
/// Lesson field
/// </summary>
public interface ILessonField : IEntity, IOrderable, IHasTitle
{
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Is required?
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// Field type
    /// </summary>
    public FieldType Type { get; set; }
}
