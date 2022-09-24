using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Contracts
{
    internal interface IBaseRepository<T>
    {
        T Create(T entity);
        T Update(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetById(Guid entityId);
    }
}
