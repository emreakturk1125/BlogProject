﻿using EA.BlogProject.Data.Concrete.EntityFramework.Contexts;
using EA.BlogProject.Data.UnitOfWork;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Services.Abstract;
using EA.BlogProject.Services.Concrete;
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
            serviceCollection.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>(); 
            serviceCollection.AddScoped<ICommentService, CommentManager>();
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


            }).AddEntityFrameworkStores<BlogContext>();
            return serviceCollection;
        }
    }
}