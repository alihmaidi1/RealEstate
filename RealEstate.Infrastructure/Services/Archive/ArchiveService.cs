// Licensed to the .NET Foundation under one or more agreements.

using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RealEstate.Shared.Abstraction.Entities;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Infrastructure.Services.Archive;

public class ArchiveService: IArchiveService
{
    private readonly RealEstateDbContext _context;
    private readonly JsonSerializerOptions jsonoptions;

    public ArchiveService(RealEstateDbContext context)
    {
        jsonoptions=new JsonSerializerOptions
        {
            
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.Preserve
        };;
        _context = context;
    }

    public async Task<Result<bool>> ArchiveEntityAsync<TEntity>(Guid Id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        where TEntity : class, IEntity
    {
        
        var query = _context.Set<TEntity>().AsQueryable();
        
        foreach (var include in includes)
        {
            query = include(query);

        }
            
        var entityWithRelations=await query
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id ==Id);
        
        if (entityWithRelations == null)
            return Result<bool>.SetError("Entity not found");

        var jsonData = JsonSerializer.Serialize(entityWithRelations, jsonoptions);
        var archiveRecord = new ArchiveRecord
        {
            EntityName = typeof(TEntity).Name,
            EntityId = Id,
            JsonData = jsonData,
            ArchivedAt = DateTime.UtcNow,
        };
        _context.ArchiveRecords.Add(archiveRecord);
        _context.Set<TEntity>().Remove(entityWithRelations);
        return Result<bool>.SetSuccess();
    }

    public async Task<Result<TEntity>> RestoreEntityAsync<TEntity>(Guid id) where TEntity : class, IEntity
    {

        var archiveRecord = await _context.ArchiveRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.EntityId == id && a.EntityName == typeof(TEntity).Name);
        
        if (archiveRecord == null)
            return Result<TEntity>.SetError("Archived record not found");


        var entity = JsonSerializer.Deserialize<TEntity>(archiveRecord.JsonData, jsonoptions);
        if (entity == null)
            return Result<TEntity>.SetError($"{typeof(TEntity).Name} Deserialization failed");
        _context.Set<TEntity>().Add(entity);
        _context.ArchiveRecords.Remove(archiveRecord);
        return Result<TEntity>.SetSuccess(entity);
    }



    public async Task<Result<bool>> DeleteArchiveAsync<TEntity>(Guid id)
        where TEntity : class, IEntity
    {
        var archiveRecord = await _context.ArchiveRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.EntityId == id && a.EntityName == typeof(TEntity).Name);

        if (archiveRecord == null)
            return Result<bool>.SetError("Archived record not found");

        _context.ArchiveRecords.Remove(archiveRecord);

        return Result<bool>.SetSuccess();

    }

    public async Task<Result<List<TEntity>>> GetArchivesAsync<TEntity>()
        where TEntity : class, IEntity
    {
        var entityName = typeof(TEntity).Name;
        var entities = _context.
            ArchiveRecords
            .AsNoTracking()
            .Where(x=>x.EntityName==entityName)
            .Select(x=>JsonSerializer.Deserialize<TEntity>(x.JsonData, jsonoptions)!)
            .ToList();

        return Result<List<TEntity>>.SetSuccess(entities);
    }

}
