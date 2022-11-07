using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EA.BlogProject.Entities.Dtos;

namespace EA.BlogProject.WebUI.Models
{
    public class ArticleSearchViewModel
    {
        public ArticleListDto ArticleListDto { get; set; }
        public string Keyword { get; set; }
    }
}
