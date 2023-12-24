﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.SDK.Common;
using Lms.SDK.Services;
using Microsoft.EntityFrameworkCore;

namespace Lms.Auth.Services;

/// <summary>
/// Base entity service
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EntityServiceBase<T> : IEntityService<T> where T : Entity
{
    protected readonly DataContext Db;
    protected readonly IMapper Mapper;

    public EntityServiceBase(DataContext db, IMapper mapper)
    {
        Db = db;
        Mapper = mapper;
    }

    public virtual IQueryable<T> GetQuery() => Db.Set<T>().AsQueryable();

    public virtual Task<TResponse> Get<TResponse>(long id, CancellationToken cancellationToken = default) where TResponse : IResponse
    {
        return GetQuery()
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<TResponse>> GetByIdRange<TResponse>(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        where TResponse : IResponse
    {
        return await GetQuery()
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken);
    }

    public virtual Task<T> Load(long id, bool tracking = false, CancellationToken cancellationToken = default)
    {
        return Db.Set<T>()
            .AsTracking(tracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking)
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public Task<long> Create<TPostRequest>(TPostRequest request, CancellationToken cancellationToken = default) where TPostRequest : IPostRequest
    {
        var entity = Mapper.Map<T>(request);
        return Create(entity, cancellationToken);
    }

    public virtual async Task<long> Create(T entity, CancellationToken cancellationToken = default)
    {
        await Db.AddAsync(entity, cancellationToken);
        await Db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<T> Update<TPutRequest>(long id, TPutRequest request, CancellationToken cancellationToken = default) where TPutRequest : IPutRequest
    {
        var entity = await Db.Set<T>()
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException($"{typeof(T).Name}: {id}");

        Db.Set<T>().Entry(entity).CurrentValues.SetValues(request);
        return await Update(entity, cancellationToken);
    }

    public virtual async Task<T> Update(T entity, CancellationToken cancellationToken = default)
    {
        Db.Update(entity);
        await Db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task Delete(long id, CancellationToken cancellationToken = default)
    {
        var entity = await Db.Set<T>()
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            ?? throw new KeyNotFoundException($"{typeof(T).Name}: {id}");

        await Delete(entity, cancellationToken);
    }

    public virtual async Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        Db.Remove(entity);
        await Db.SaveChangesAsync(cancellationToken);
    }
}

