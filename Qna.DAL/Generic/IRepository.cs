using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(params object[] id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<bool> RemoveByIdAsync(params object[] id);
    }
}
