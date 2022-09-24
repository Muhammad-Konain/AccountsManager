using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private AppDBContext _context;
        public BaseRepository(AppDBContext context)
        {
            _context = context;
        }
        public virtual T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public virtual T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public virtual IQueryable<T> GetById(Guid entityID)
        {
            return _context.Set<T>()
                           .Where(w => w.Id == entityID)
                           .AsQueryable();
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
