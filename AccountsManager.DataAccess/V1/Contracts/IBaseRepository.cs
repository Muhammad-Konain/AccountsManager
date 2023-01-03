using System.Linq.Expressions;

namespace AccountsManager.DataAccess.V1.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entity);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entity);
        T Delete(T entity);
        IEnumerable<T> DeleteRange(IEnumerable<T> entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetById(Guid entityId);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
