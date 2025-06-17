using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Shared.Abstraction.Entities;
using RealEstate.Shared.Abstraction.Entities.Entity;

namespace RealEstate.Infrastructure.Repositories.Base.Repository;

public interface IBaseRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();

}
