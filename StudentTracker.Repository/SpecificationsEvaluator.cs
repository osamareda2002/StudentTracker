using Microsoft.EntityFrameworkCore;
using StudentTracker.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery; //_dbContext.Set<T>()
            if(spec.Criteria is not  null) 
            {
                query = query.Where(spec.Criteria); //where (p => p.Id == 1)
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            //.Include(p => p.prob)
            return query;

        }
    }
}
