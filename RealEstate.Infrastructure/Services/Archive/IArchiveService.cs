// Licensed to the .NET Foundation under one or more agreements.

using System.Linq.Expressions;
using RealEstate.Shared.Abstraction.Entities;
using RealEstate.Shared.Abstraction.Entities.Entity;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Infrastructure.Services.Archive;

public interface IArchiveService
{

    public Task<TResult<bool>> ArchiveEntityAsync<TEntity>(Guid Id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        where TEntity : class, IEntity;

    public Task<TResult<TEntity>> RestoreEntityAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<TResult<bool>> DeleteArchiveAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<TResult<List<TEntity>>> GetArchivesAsync<TEntity>()
        where TEntity : class, IEntity;



}
