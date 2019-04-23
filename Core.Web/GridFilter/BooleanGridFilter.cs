﻿using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class BooleanGridFilter<T> : BaseGridFilter<T>
    {
        private readonly Expression<Func<T, bool>> expression;

        public BooleanGridFilter(Expression<Func<T, bool>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }
    }
}