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

            query = sepc.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
