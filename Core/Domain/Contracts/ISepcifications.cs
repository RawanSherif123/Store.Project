﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISepcifications<TEntity , TKey > where TEntity : BaseEntity<TKey>
    {
         Expression<Func<TEntity , bool >> ?  Criteria { get; set; }
      public  List<Expression<Func<TEntity, object>>?> IncludeExpressions { get; set; }

    }
}
