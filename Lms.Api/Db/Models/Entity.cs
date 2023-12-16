using Lms.SDK.Common;

namespace Lms.Api.Db.Models;

/// <summary>
/// Entity class
/// </summary>
public class Entity : IEntity
{
    public required long Id { get; set; }

    public override string ToString() => $"{GetType().Name}: {Id}";
}

