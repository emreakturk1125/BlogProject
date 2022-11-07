using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Entities.Dtos;

namespace EA.BlogProject.WebUI.Models
{
    public class ArticleDetailRightSideBarViewModel
    {
        public string Header { get; set; }
        public ArticleListDto ArticleListDto { get; set; }
        public User User { get; set; }
    }
}
