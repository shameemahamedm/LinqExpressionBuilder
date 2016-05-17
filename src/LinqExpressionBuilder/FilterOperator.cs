// -----------------------------------------------------------------------
// <copyright file="FilterOperator.cs" company="Shameem">
// Copyright (c) Shameem Ahamed. All rights reserved.
// </copyright>
// <author>Shameem Ahmed</author>
// <date>17/05/2016</date>
// -----------------------------------------------------------------------

namespace LinqExpressionBuilder
{
    public enum FilterOperator
    {
        /// <summary>
        ///     The none
        /// </summary>
        None = 0,

        /// <summary>
        ///     The equals
        /// </summary>
        Equals = 1,

        /// <summary>
        ///     The contains
        /// </summary>
        Contains = 2,

        /// <summary>
        ///     The Does Not Contains
        /// </summary>
        DoesnotContains = 6,

        /// <summary>
        ///     The starts with
        /// </summary>
        StartsWith = 3,

        /// <summary>
        ///     The ends with
        /// </summary>
        EndsWith = 4,

        /// <summary>
        ///     The not equals
        /// </summary>
        NotEquals = 5
    }
}