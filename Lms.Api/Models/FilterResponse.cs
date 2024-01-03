using Lms.SDK.Common;

namespace Lms.Api.Models;

/// <summary>
/// Filter response class
/// </summary>
public class FilterResponse<TResponse> : IFilterResponse<TResponse> where TResponse: IResponse
{
    public long Total { get; set; }
    public required IEnumerable<TResponse> Items { get; set; }
}
