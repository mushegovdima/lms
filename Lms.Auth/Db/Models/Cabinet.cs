using Lms.SDK.Interfaces;

namespace Lms.Auth.Db.Models;

/// <summary>
/// Cabinet 
/// </summary>
public class Cabinet : Entity, ICabinet
{
    public required string Title { get; set; }
    public required long AuthorId { get; set; }
    public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}
