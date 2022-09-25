using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.Abstract
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
    }
}
