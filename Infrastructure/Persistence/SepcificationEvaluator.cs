using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SepcificationEvaluator
    {
        // Generate Query
        public static IQueryable<TEntity> GetQuery<TEntity , TKey>(
            IQueryable<TEntity> inputQuery, 
            ISepcifications<TEntity, TKey> sepc)
            where TEntity : BaseEntity<TKey>

        {
            var query = inputQuery;
            if (sepc.Criteria is not null)
             query  =   query.Where(sepc.Criteria);

            if (sepc.OrderBy is not null)
            query = query.OrderBy(sepc.OrderBy);
            else if (sepc.OrderByDescending is not null )
                query = query.OrderByDescending(sepc.OrderByDescending);

            if ( sepc.IsPagination)
                query = query.Skip(sepc.Skip).Take(sepc.Take);


            query = sepc.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
