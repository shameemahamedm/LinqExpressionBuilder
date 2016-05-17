// -----------------------------------------------------------------------
// <copyright file="ExpressionBuilder.cs" company="Shameem">
// Copyright (c) Shameem Ahamed. All rights reserved.
// </copyright>
// <author>Shameem Ahmed</author>
// <date>17/05/2016</date>
// -----------------------------------------------------------------------

namespace LinqExpressionBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    ///     Build Dynamic LINQ Query
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        ///     The contains method
        /// </summary>
        private static readonly MethodInfo ContainsMethod = typeof (string).GetMethod("Contains");

        /// <summary>
        ///     The starts with method
        /// </summary>
        private static readonly MethodInfo StartsWithMethod =
            typeof (string).GetMethod("StartsWith", new[] {typeof (string)});

        /// <summary>
        ///     The ends with method
        /// </summary>
        private static readonly MethodInfo EndsWithMethod =
            typeof (string).GetMethod("EndsWith", new[] {typeof (string)});

        /// <summary>
        ///     Gets the expression.
        /// </summary>
        /// <typeparam name="T">Filter Type</typeparam>
        /// <param name="filters">The filters.</param>
        /// <returns>filter expression</returns>
        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters == null)
            {
                return null;
            }

            if (filters.Count == 0)
            {
                return null;
            }

            var param = Expression.Parameter(typeof (T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = GetExpression<T>(param, filters[0]);
            }
            else if (filters.Count == 2)
            {
                exp = GetExpression<T>(param, filters[0], filters[1]);
            }
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                    {
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        /// <summary>
        ///     Gets the expression.
        /// </summary>
        /// <typeparam name="T">Filter Type</typeparam>
        /// <param name="param">The parameter.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>filter expression</returns>
        private static Expression GetExpression<T>(Expression param, Filter filter)
        {
            Expression expression = null;

            //// Property Field
            var member = Expression.Property(param, filter.PropertyName);

            //// Value Field as Lower case string
            var constant = Expression.Constant(filter.Value.ToString().ToLower());

            //// Convert Property Value as lower case
            var toLower = Expression.Call(member, typeof (string).GetMethod("ToLower"));

            Expression checkNull = Expression.NotEqual(member, Expression.Constant(null, member.Type));

            switch (filter.Operation)
            {
                case FilterOperator.Equals:
                    expression = Expression.AndAlso(checkNull, Expression.Equal(toLower, constant));
                    break;

                case FilterOperator.NotEquals:
                    expression = Expression.AndAlso(checkNull, Expression.NotEqual(toLower, constant));
                    break;

                case FilterOperator.Contains:
                    expression = Expression.AndAlso(checkNull, Expression.Call(toLower, ContainsMethod, constant));
                    break;
                case FilterOperator.DoesnotContains:
                    expression = Expression.Not(Expression.Call(toLower, ContainsMethod, constant));
                    break;

                case FilterOperator.StartsWith:
                    expression = Expression.AndAlso(checkNull, Expression.Call(toLower, StartsWithMethod, constant));
                    break;

                case FilterOperator.EndsWith:
                    expression = Expression.AndAlso(checkNull, Expression.Call(toLower, EndsWithMethod, constant));
                    break;
            }

            return expression;
        }

        /// <summary>
        ///     Gets the expression.
        /// </summary>
        /// <typeparam name="T">Filter Type</typeparam>
        /// <param name="param">The parameter.</param>
        /// <param name="filter1">The filter1.</param>
        /// <param name="filter2">The filter2.</param>
        /// <returns>filter expression</returns>
        private static BinaryExpression GetExpression<T>(Expression param, Filter filter1, Filter filter2)
        {
            var bin1 = GetExpression<T>(param, filter1);
            var bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
}