using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDBContext _context;
        public BaseRepository(AppDBContext context)
        {
            _context = context;
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.Set<T>()
                          .AddAsync(entity);

            return entity;
        }
        public virtual async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.LastModifiedOn = DateTime.UtcNow;

            }
            await _context.Set<T>()
                          .AddRangeAsync(entities);

            return entities;
        }
        public virtual T Update(T entity)
        {
            entity.LastModifiedOn = DateTime.UtcNow;

            _context.Set<T>().Update(entity);
            return entity;
        }
        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.LastModifiedOn = DateTime.UtcNow;
            }

            _context.Set<T>().UpdateRange(entities);

            return entities;
        }
        public virtual T Delete(T entity)
        {
            entity.IsActive = false;
            entity.DeletedOn = DateTime.UtcNow;

            _context.Set<T>().Update(entity);
            return entity;
        }
        public virtual IEnumerable<T> DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.DeletedOn = DateTime.UtcNow;
            }

            _context.Set<T>().UpdateRange(entities);
            return entities;
        }
        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>()
                           .Where(w => w.IsActive);
        }
        public virtual IQueryable<T> GetById(Guid entityID)
        {
            return _context.Set<T>()
                           .Where(w => w.Id == entityID && w.IsActive);
        }
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                           .Where(w =>w.IsActive)
                           .Where(predicate);

        }
    }
}
