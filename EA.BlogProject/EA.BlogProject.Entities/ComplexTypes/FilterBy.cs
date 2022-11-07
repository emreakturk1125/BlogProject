using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Entities.ComplexTypes
{
    public enum FilterBy
    {
        Category = 0, //GetAllByUserIdOnDate(FilterBy=FilterBy.Category,int categoryId)
        Date = 1,
        ViewCount = 2,
        CommentCount = 3
    }
}
