namespace Lms.SDK.Common;

/// <summary>
/// Soft deletable entity
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Flag is true when deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}
