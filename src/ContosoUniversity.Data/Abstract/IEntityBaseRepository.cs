using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
 
   
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<int> CountAsync();
        Task CommitAsync();
        Task AddAsync(T entity);

        void Update(T entity);       
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        
        
    }
}