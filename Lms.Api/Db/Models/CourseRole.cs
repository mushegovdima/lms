using Lms.SDK.Enums;

namespace Lms.Api.Db.Models;

/// <summary>
/// Course permissions
/// </summary>
public class CourseRole: Entity
{
    public long UserId { get; set; }
    public long CourseId { get; set; }
    public Role Role { get; set; }
    public Course? Course { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
