namespace Lms.SDK.Common;

/// <summary>
/// Filter interface
/// </summary>
public interface IFilter<T> where T: IEntity
{
    /// <summary>
    /// page number
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Order field
    /// </summary>
    public string OrderBy { get; set; }

    /// <summary>
    /// Sort by descending?
    /// </summary>
    public bool Desc { get; set; }
}
