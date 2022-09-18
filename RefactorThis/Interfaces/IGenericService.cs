using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.Interfaces
{
    public interface IGenericService<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        void Save(TEntity entity);
        Task SaveAsync(TEntity entity);
        
    }
}