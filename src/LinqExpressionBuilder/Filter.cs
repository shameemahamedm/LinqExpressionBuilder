// -----------------------------------------------------------------------
// <copyright file="Filter.cs" company="Shameem">
// Copyright (c) Shameem Ahamed. All rights reserved.
// </copyright>
// <author>Shameem Ahmed</author>
// <date>17/05/2016</date>
// -----------------------------------------------------------------------

namespace LinqExpressionBuilder
{
    public class Filter
    {
        /// <summary>
        ///     Gets or sets the name of the property.
        /// </summary>
        /// <value>
        ///     The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        ///     Gets or sets the operation.
        /// </summary>
        /// <value>
        ///     The operation.
        /// </value>
        public FilterOperator Operation { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public object Value { get; set; }
    }
}