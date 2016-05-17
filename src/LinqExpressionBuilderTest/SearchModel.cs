using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqExpressionBuilderTest
{
    public class SearchModel
    {
        public int Id { get; set; }

        public string ReferenceNumber { get; set; }

        public string UserId { get; set; }

        public Nullable<System.DateTime> CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public string StatusArabic { get; set; }

        public string StatusEnglish { get; set; }

        public string CategoryArabic { get; set; }

        public string CategoryEnglish { get; set; }

        public string SubCategoryArabic { get; set; }

        public string SubCategoryEnglish { get; set; }
    }
}
