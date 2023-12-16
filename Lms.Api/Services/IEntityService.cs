using Lms.Api.Dto;
using Lms.SDK.Common;

namespace Lms.Api.Services;

/// <summary>
/// Entity service interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityService<T> where T: IEntity
{
    /// <summary>
    /// Get query
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> GetQuery();

    /// <summary>
    /// Load entity with params
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> Load(long id, bool tracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get dto by id
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TResponse> Get<TResponse>(long id, CancellationToken cancellationToken = default)
        where TResponse: IResponse;

    /// <summary>
    /// Get dto`s by id range
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TResponse>> GetByIdRange<TResponse>(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        where TResponse : IResponse;

    /// <summary>
    /// Create entity from dto
    /// </summary>
    /// <typeparam name="TPostRequest"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<long> Create<TPostRequest>(TPostRequest request, CancellationToken cancellationToken = default)
        where TPostRequest : IPostRequest;

    /// <summary>
    /// Create entity from entity model
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<long> Create(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity from request (dto)
    /// </summary>
    /// <typeparam name="TPutRequest"></typeparam>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> Update<TPutRequest>(long id, TPutRequest request, CancellationToken cancellationToken = default)
        where TPutRequest : IPutRequest;

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> Update(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete entitity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Delete(long id, CancellationToken cancellationToken = default);
}