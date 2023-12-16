using System.Dynamic;

namespace Lms.SDK.Common;

/// <summary>
/// Dynamic entity
/// </summary>
public interface IDynamicEntity : IEntity
{
    /// <summary>
    /// Dynamic data
    /// </summary>
    public ExpandoObject Data { get; set; }
}

