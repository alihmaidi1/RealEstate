using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RealEstate.Infrastructure.Repositories.Base;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.Repositories.Cache;

public class CachedRepositoryDecorator<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IEntity

{

    private readonly IBaseRepository<TEntity> _innerRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheOptions;
    private readonly SemaphoreSlim _cacheLock = new(1, 1);


    public CachedRepositoryDecorator(IBaseRepository<TEntity> innerRepository, IMemoryCache memoryCache)
    {
        _innerRepository = innerRepository;
        _memoryCache = memoryCache;

        _cacheOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            Priority = CacheItemPriority.Normal
        };
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var cacheKey = GetCacheKey(id);

        // Try to get from cache
        if (_memoryCache.TryGetValue<TEntity>(cacheKey, out var cachedEntity))
        {
            return cachedEntity;
        }

        // Get from inner repository
        var entity = await _innerRepository.GetByIdAsync(id);
        if (entity == null) return null;

        // Cache the result
        _memoryCache.Set(cacheKey, entity, _cacheOptions);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var cacheKey = GetAllCacheKey();

        // Try to get from cache
        if (_memoryCache.TryGetValue<IEnumerable<TEntity>>(cacheKey, out var cachedEntities))
        {
            return cachedEntities;
        }

        // Use semaphore to prevent cache stampede
        await _cacheLock.WaitAsync();
        try
        {
            // Double-check after acquiring lock
            if (_memoryCache.TryGetValue<IEnumerable<TEntity>>(cacheKey, out cachedEntities))
            {
                return cachedEntities;
            }

            // Get from inner repository
            var entities = (await _innerRepository.GetAllAsync()).ToList();

            // Cache the result
            _memoryCache.Set(cacheKey, entities, _cacheOptions);

            // Cache individual entities
            foreach (var entity in entities)
            {
                _memoryCache.Set(GetCacheKey(entity.Id), entity, _cacheOptions);
            }

            return entities;
        }
        finally
        {
            _cacheLock.Release();
        }
    }

    public async Task AddAsync(TEntity entity)
    {
        await _innerRepository.AddAsync(entity);
        InvalidateCacheForEntity(entity.Id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _innerRepository.UpdateAsync(entity);
        InvalidateCacheForEntity(entity.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _innerRepository.DeleteAsync(id);
        InvalidateCacheForEntity(id);
    }

    public Task SaveChangesAsync() => _innerRepository.SaveChangesAsync();

    private void InvalidateCacheForEntity(Guid id)
    {
        var entityKey = GetCacheKey(id);
        var allEntitiesKey = GetAllCacheKey();

        _memoryCache.Remove(entityKey);
        _memoryCache.Remove(allEntitiesKey);
    }

    private string GetCacheKey(Guid id) => $"{typeof(TEntity).Name}:{id}";
    private string GetAllCacheKey() => $"{typeof(TEntity).Name}:all";

}
