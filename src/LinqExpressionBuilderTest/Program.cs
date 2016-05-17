using LinqExpressionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqExpressionBuilderTest
{
    public class Program
    {
        public void Main(string[] args)
        {
            IList<Filter> filters = null;
            if (filters.Count <= 0)
            {
                return result;
            }

            var deleg = ExpressionBuilder.GetExpression<SearchModel>(filters).Compile();
            result = result.Where(deleg).ToList();

            return result;
        }
    }
}
