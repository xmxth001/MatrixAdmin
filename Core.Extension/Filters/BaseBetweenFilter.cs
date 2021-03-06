﻿using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.Filters
{
    public abstract class BaseBetweenFilter<T> : IFilterInfo
    {
        protected BaseBetweenFilter(string propertyName, object min, object max)
        {
            this.PropertyName = propertyName;
            this.Value = min;
            this.Value2 = max;
            this.Validate();
            this.Operation = ExpressionBuilder.Operations.Operation.Between;
            this.Connector = Connector.And;
            this.IsFilterEnable = min != null || max != null;
        }

        public virtual bool IsFilterEnable { get; set; }

        public Connector Connector { get; set; }

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public IEnumerable<IFilterInfo> FilterInfos => throw new System.NotImplementedException();

        public Expression Expression => throw new System.NotImplementedException();

        public void Validate()
        {
        }
    }
}