// Licensed to the .NET Foundation under one or more agreements.

using System.Linq.Expressions;
using RealEstate.Shared.Abstraction.Entities;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Infrastructure.Services.Archive;

public interface IArchiveService
{

    public Task<Result<bool>> ArchiveEntityAsync<TEntity>(Guid Id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        where TEntity : class, IEntity;

    public Task<Result<TEntity>> RestoreEntityAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<Result<bool>> DeleteArchiveAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<Result<List<TEntity>>> GetArchivesAsync<TEntity>()
        where TEntity : class, IEntity;



}
