using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Shared.Data.Abstract;
using EA.BlogProject.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Entities.Dtos
{
    public class ArticleListDto : DtoBase
    {
        public IList<Article> Articles { get; set; } 
        
    }
}

