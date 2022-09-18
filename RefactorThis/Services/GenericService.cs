using refactor_me.Data;
using refactor_me.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace refactor_me.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApiDatabaseContext _apiDatabaseContext;

        public GenericService(ApiDatabaseContext apiDatabaseContext)
        {
            _apiDatabaseContext = apiDatabaseContext;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var rows = _apiDatabaseContext.Set<TEntity>()
                .ToList();

            return rows;
        }

        public virtual TEntity GetById(Guid id)
        {
            return _apiDatabaseContext.Set<TEntity>().Find(id);
        }

        public virtual void Create(TEntity entity)
        {
            _apiDatabaseContext.Set<TEntity>().Add(entity);
            _apiDatabaseContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            bool existing = Exists(entity.Id);
            if (!existing)
            {
                throw new Exception($"The entity of [{entity.Id}] does not exist");
            }

            _apiDatabaseContext.Entry<TEntity>(entity).State = EntityState.Modified;
            _apiDatabaseContext.SaveChanges();
        }



        public virtual void Delete(Guid id)
        {
            var entity = _apiDatabaseContext.Set<TEntity>().Find(id);
            if (entity == null)
            {
                throw new Exception($"The entity of [{entity.Id}] does not exist");
            }

            _apiDatabaseContext.Set<TEntity>().Remove(entity);
            _apiDatabaseContext.SaveChanges();
        }


        protected virtual bool Exists(Guid id)
        {
            var entity = _apiDatabaseContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            return entity != null;
        }


        public virtual void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception($"The input entity is null");
            }

            if (entity.Id.Equals(default(Guid)))
            {
                _apiDatabaseContext.Set<TEntity>().Add(entity);
            }
            else
            {
                bool existing = Exists(entity.Id);
                if (!existing)
                {
                    throw new Exception($"The entity of [{entity.Id}] does not exist");
                }

                _apiDatabaseContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            _apiDatabaseContext.SaveChanges();
        }

        #region async methods

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var rows = await _apiDatabaseContext.Set<TEntity>()
                .ToListAsync();

            return rows;
        }


        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var row = await _apiDatabaseContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return row;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            _apiDatabaseContext.Set<TEntity>().Add(entity);
            await _apiDatabaseContext.SaveChangesAsync();
        }


        public virtual async Task SaveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception($"The input entity is null");
            }

            if (entity.Id.Equals(default(Guid)))
            {
                _apiDatabaseContext.Set<TEntity>().Add(entity);
            }
            else
            {
                bool existing = await ExistsAsync(entity.Id);
                if (!existing)
                {
                    throw new Exception($"The entity of [{entity.Id}] does not exist");
                }

                _apiDatabaseContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            await _apiDatabaseContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            bool existing = Exists(entity.Id);
            if (!existing)
            {
                throw new Exception($"The entity of [{entity.Id}] does not exist");
            }

            _apiDatabaseContext.Entry<TEntity>(entity).State = EntityState.Modified;
            await _apiDatabaseContext.SaveChangesAsync();
        }


        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _apiDatabaseContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"The entity of [{id}] does not exist");
            }

            _apiDatabaseContext.Set<TEntity>().Remove(entity);
            await _apiDatabaseContext.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var entity = await _apiDatabaseContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity != null;
        }

        #endregion async methods

    }
}
