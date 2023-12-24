using Lms.SDK.Enums;

namespace Lms.Auth.Db.Models;

public class CabinetRole: Entity
{
    public long UserId { get; set; }
    public long CabinetId { get; set; }
    public Role Role { get; set; }
    public User? User { get; set; }
    public Cabinet? Cabinet { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
