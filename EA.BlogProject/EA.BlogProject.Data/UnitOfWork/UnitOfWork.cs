using EA.BlogProject.Data.Abstract;
using EA.BlogProject.Data.Concrete.EntityFramework.Contexts;
using EA.BlogProject.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;
        private EfArticleRepository _efArticleRepository;
        private EfCategoryRepository  _efCategoryRepository;
        private EfCommentRepository  _efCommentRepository; 

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        // ??=  =>  _efArticleRepository  boş ise yeni bir instance oluştur ve onu _efArticleRepository'a ata demektir.
        public IArticleRepository Articles => _efArticleRepository ??= new EfArticleRepository(_context);

        public ICategoryRepository Categories => _efCategoryRepository ??= new EfCategoryRepository(_context);   

        public ICommentRepository Comments => _efCommentRepository ??= new EfCommentRepository(_context);    
            
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
         
    }
}
