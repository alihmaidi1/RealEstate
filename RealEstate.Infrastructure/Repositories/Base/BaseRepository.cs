using Microsoft.EntityFrameworkCore;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {

        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {

        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {

        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null) _context.Set<TEntity>().Remove(entity);

    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


}
