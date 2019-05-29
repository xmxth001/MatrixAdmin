﻿using System;

namespace Dapper
{
    /// <summary>
    /// Optional Editable attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the properties that are editable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableAttribute"/> class.
        /// Optional Editable attribute.
        /// </summary>
        /// <param name="iseditable"></param>
        public EditableAttribute(bool iseditable)
        {
            this.AllowEdit = iseditable;
        }

        /// <summary>
        /// Does this property persist to the database?.
        /// </summary>
        public bool AllowEdit { get; private set; }
    }
}