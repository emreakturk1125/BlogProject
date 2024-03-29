﻿using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Mvc.AutoMapper.Profiles;
using EA.BlogProject.Mvc.Helpers.Abstract;
using EA.BlogProject.Mvc.Helpers.Concrete;
using EA.BlogProject.Services.AutoMapper.Profiles;
using EA.BlogProject.Services.Extensions;
using EA.BlogProject.Shared.Utilities.Extensions;
using EA.BlogProject.WebUI.AutoMapper.Profiles;
using EA.BlogProject.WebUI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EA.BlogProject.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }    
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));          //burada config'den veri okuma
            services.Configure<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));

            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));  //burada ise config'e veri yazma
            services.ConfigureWritable<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));

            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boş geçilmemelidir.");
                options.Filters.Add<MvcExceptionFilter>();
            }).AddRazorRuntimeCompilation()              // AddRazorRuntimeCompilation => front tarafındaki her değişiklikte anlık yansımasını sağlamak için
                .AddJsonOptions(opt =>                     // Json işlemleri için
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());  // Enum dönüştürücü 
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;   // Json a dönüştürdüğümüz değer içinde farklı değerler varsa onlarında gönderilmesi için (controller içinde de bu değeri eklememiz gerekiyor şimdilik bugfix olduğu için)
            }).AddNToastNotifyToastr();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(ViewModelsProfile), typeof(CommentProfile));
            services.LoadMyServices(Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");                  // Kullanıcı girişi yapmadan admin area ya gitmek isterse login sayfasına yönlecek
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder()
                {
                    Name = "BlogProject",
                    HttpOnly = true,                   // Cookie bilgilerinin erişme engellenmesi sağlanır
                    SameSite = SameSiteMode.Strict,    // Cookie bilgilerini aynı siteden yani bizim sitemiz üzerinden gelen cookie bilgilerine göre işlem yapar
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);  // Cookie bilgileri 7 gün geçerli olacak
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
            });
        }

        // Configure içinde tanımladığın middleware lerin sıralaması önemlidir.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/sayfabulunamadi";
                    await next();   
                }
            });

            app.UseSession(); 
            app.UseStaticFiles(); 
            app.UseRouting(); 
            app.UseAuthentication(); 
            app.UseAuthorization(); 
            app.UseNToastNotify(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name : "article",
                    pattern : "{title}/{articleId}",
                    defaults: new { controller = "Article", action = "Detail",}
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
