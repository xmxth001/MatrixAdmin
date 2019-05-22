﻿using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Identifiers;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<T> : BaseGridFilter
    {
        public DateTimeGridFilter(Expression<Func<T, DateTime?>> expression, string label, string tooltip = default) : base(label, expression.GetPropertyName(), tooltip: tooltip)
        {
        }

        public override string Render()
        {
            string tooltip = this.Tooltip == default ? string.Empty : $"data-toggle=\"tooltip\" data-placement=\"top\" title=\"{this.Tooltip}\"";
            string id = new Identifier().Value;

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label for=\"{id}\" {tooltip}>{this.LabelText}</label>" +
                   $"<input class=\"form_datetime form-control\" name=\"{this.InputName}\" type=\"text\" id=\"{id}\">" +
                   $"</div>" +
                   $"</div>";
        }
    }
}