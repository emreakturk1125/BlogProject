using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EA.BlogProject.Entities.Concrete;

namespace EA.BlogProject.WebUI.Models
{
    public class RightSideBarViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
