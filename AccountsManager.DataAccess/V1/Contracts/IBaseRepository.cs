using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<T> Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetById(Guid entityId);
    }
}
