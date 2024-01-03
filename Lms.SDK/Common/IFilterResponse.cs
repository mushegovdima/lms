namespace Lms.SDK.Common;

public interface IFilterResponse<T> where T: IResponse
{
    public long Total { get; set; }
    public IEnumerable<T> Items { get; set; }
}
