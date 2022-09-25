using EA.BlogProject.Data.Abstract;
using EA.BlogProject.Data.Concrete.EntityFramework.Contexts;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.Concrete.EntityFramework.Repositories
{
    // _context miras alınan EfEntityRepositoryBase  classında protected olarak tanımladığım nesnedir.
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {

        }
        private BlogContext BlogContext
        {
            get { return _context as BlogContext; }
        }
        public async Task<Category> GetByIdAsync(int categoryId)
        {
            return await BlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }
          
    } 
}
