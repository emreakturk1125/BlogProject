using EA.BlogProject.Data.Concrete.EntityFramework.Mappings;
using EA.BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.Concrete.EntityFramework.Contexts
{
    // Users ve Roles Identity ile birlikte geleceği için burada tanımlamıyoruz.Identity tabloları olmasaydı tanımlardık
    // Identity den gelecek User ve Role tablolarının primary key lerini int olmasını istedik
    public class BlogDbContext : IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Log> Logs { get; set; }

        //public DbSet<Role> Roles { get; set; }     

        //public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=TKARGCLNMS28;Database=EABLOGDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        //}

        // Veritabanı oluşturulurken, Fluent mapping işlemini tanımlandığı yer(tablo tanımlarını)
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
              
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());

        }
    }
}

