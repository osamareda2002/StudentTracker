using StudentTracker.Core.Entities;
using StudentTracker.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?>GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        void Update(T entity);
    }
}
