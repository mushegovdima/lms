﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lms.Api.Db;
using Lms.Api.Db.Models;
using Lms.Api.Dto;
using Lms.SDK.Common;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Services;

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

    public IQueryable<T> GetQuery() => Db.Set<T>().AsQueryable();


    public Task<TResponse> Get<TResponse>(long id, CancellationToken cancellationToken = default) where TResponse : IResponse
    {
        return GetQuery()
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TResponse>> GetByIdRange<TResponse>(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        where TResponse : IResponse
    {
        return await GetQuery()
            .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
            .Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken);
    }

    public Task<T> Load(long id, bool tracking = false, CancellationToken cancellationToken = default)
    {
        return Db.Set<T>()
            .AsTracking(tracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking)
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<long> Create<TPostRequest>(TPostRequest request, CancellationToken cancellationToken = default) where TPostRequest : IPostRequest
    {
        var entity = Mapper.Map<T>(request);
        await Db.AddAsync(entity, cancellationToken);
        await Db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<long> Create(T entity, CancellationToken cancellationToken = default)
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

    public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
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

        Db.Remove(entity);
        await Db.SaveChangesAsync(cancellationToken);
    }
}
