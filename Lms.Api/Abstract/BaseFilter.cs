using AutoMapper;
using Lms.SDK.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;
using Lms.Api.Models;

namespace Lms.Api.Abstract;

/// <summary>
/// Base filter class
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class BaseFilter<T, TResponse> : IFilter<T>
    where T: IEntity
    where TResponse : IResponse
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string OrderBy { get; set; } = string.Empty;
    public bool Desc { get; set; }

    /// <summary>
    /// Apply filters to query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected abstract IQueryable<T> ApplyFilter(IQueryable<T> query);

    /// <summary>
    /// Get response for query and params
    /// </summary>
    /// <param name="query"></param>
    /// <param name="mapper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IFilterResponse<TResponse>> GetResponse(IQueryable<T> query, IMapper mapper, CancellationToken cancellationToken)
    {
        query = ApplyFilter(query); // apply filter

        var total = await query.CountAsync(cancellationToken); // get total
        if (total <= 0)
            return new FilterResponse<TResponse>() { Total = total, Items = Enumerable.Empty<TResponse>() } ;

        if (string.IsNullOrWhiteSpace(OrderBy) && typeof(T).GetProperty(OrderBy) is not null)
            query = Desc
                ? query.OrderBy(OrderBy + " descending")
                : query.OrderBy(OrderBy);
        
        var items = await query
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .ProjectTo<TResponse>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return new FilterResponse<TResponse>() { Total = total, Items = items } ;
    }
}
