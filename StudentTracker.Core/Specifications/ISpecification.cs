﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; } //lambda exp value of where
        public List<Expression<Func<T, object>>> Includes { get;}
    }
}
