using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Entities.Dtos
{
    public class CategoryDto : DtoBase
    {
        public Category Category { get; set; }
    }
}
