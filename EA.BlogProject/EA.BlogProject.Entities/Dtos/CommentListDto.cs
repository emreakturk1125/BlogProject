using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EA.BlogProject.Entities.Concrete;

namespace EA.BlogProject.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
