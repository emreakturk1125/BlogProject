using EA.BlogProject.Data.Concrete.EntityFramework.Contexts;
using EA.BlogProject.Data.UnitOfWork;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Services.Abstract;
using EA.BlogProject.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Services.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)); 
            serviceCollection.AddIdentity<User,Role>(options => 
            {
                options.Password.RequireDigit = false;            // Şifre herhangibir rakam içermek zorunda değil
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;    
                options.Password.RequireNonAlphanumeric = false;  // Özel karakter kullanımı zorunlu değil
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;           // Uniqe email adresi olması için


            }).AddEntityFrameworkStores<BlogDbContext>();

            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>   // SecurityStamp önemli değerlerin değiştiğini gösterir
            {
                options.ValidationInterval = TimeSpan.FromSeconds(15);              // bir kullanıcıya rol atandığı zaman 15 dk içinde logout olacak ve tekrardan giriş yapılması istenecek
            });
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();       // AddScoped    => Her istek için yeni bir manager instance üretilir
            serviceCollection.AddSingleton<IMailService, MailManager>();          // AddSingleton => Her bir istek için yeni bir manager sınıfına ihtiyacımız yok

            return serviceCollection;
        }
    }
}
