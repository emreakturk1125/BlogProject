# BlogProject
  
  
-> .Net 5.0 <br>
-> UnitOfWork <br>
-> Generic Repository <br>
-> Asenkron Programming <br>
-> AutoMapper <br>
-> WebUI kısmında "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompil" kütüphanesi, frontend tarafında değişikliklerin anlık yansını sağlamak için <br>
-> Toastr <br>
-> (Toastr) EA.BlogProject.Shared, class library içine  "Microsoft.AspNetCore.Mvc" kütüphanesini ekledik. Toaster ile alaklı controller ekledik. Farklı projelerde de kullanılabilsin diye yani shared katmmanın başka projelere ekle ve kullan diye <br>
-> SweetAlert <br>
-> Asp.Net Identity <br>
-> Trumbowyg  (jquery text editörü), <br>
-> Select2 (searchable dropdownlist) <br>
-> NToastNotify (Bildirim işlemleri için kullanılabilir) <br>
-> Authorization <br>
-> Give Authorize to Users any pages from Web Pages (Sayfa üzerinde kullanıcılara yetki verme) <br>
-> LinqKit kütüphanesi ile Ana sayfada ki Search kısımı için gereken işlemleri yaptık <br>
-> Bootsnipp.com dan farklı şablonlar bulabilirsin. <br>
-> Mail işlemleri için outlook smtp  <br>
-> NLog kütüphanesi (https://github.com/NLog/NLog/wiki/Database-target)  => File & database log  <br>

    * log atılacak tablonun create edimesi gerekir. Dokümentasyon için "NLog and SQL Server Example Configuration" başlığı altında fakat bizim code first olduğu için "Log" class'ı tanımladık ve migration işlemi yaptık <br>
    * NLog, NLog.Web.AspNetCore, NLog.Database  paketlerini kurduk <br>
    * WebUI içine nlog adında config dosyası ekledik (https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5) <br>
    * File & database, her iki log işlemi de olsun diye nlog.config de düzenleme yaptık <br>
    * Program.cs de dğer loggerları disabled etmemiz gerekiyor, nlog u kullanabilmek için  <br>


-> Appsetting.config ile ilgili işlemler  (hem okuma hem de üzerine yazma işlemleri) <br>

    * Appsettings.json dosyasından farklı yolla veri okuma işlemi yapıldı <br>
    * WritetibleOptions adlı helper sınıfı  appsettings.json üzerine veri yazmayı sağlar  => ( EA.BlogProject.Shared içinde ) <br>
    * WritetibleOptions için "Microsoft.Extensions.Options.ConfigurationExtensions" kütüphanesinin de kurulması gerekir  <br>

-> AsNoTracking işlemi => EfEntityRepositoryBase içindeki sorgularda kullanıldı.  <br>
   
    * Sadece çekilen tabloyu getir ya da include edilen tablo varsa onunla birlikte getir demektir. <br>
    * Birbirini referans eden kayıtların tekrar etmesini engelledik, büyük bir performans kazancı oluoyoruz. <br>
    * AsNoTracking işlemi 1. farklı bir yöntem olarak alttaki şekilde de yapılabilir, yani  sorguların sonuna AsNoTracking() koymak yerine constructor da alttaki gibi kod eklenebilir. <br>
    *  Bu sorgulara otomatik AsNoTracking ekler.  <br>
    // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;  <br>
    * AsNoTracking işlemi 2. farklı bir yöntem olarak; Startup da tanımlanan DbContext sin sonuna alttaki kod parççaığı eklenebilir  <br>
    // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) <br>

-> FirstOrDefault() ile SingleOrDefault() farkı nedir? <br>
   
   * SingleOrDefault; Yeni bir sorgu üretir. <br>
   * FirstOrDefault; EntityFramework de cache mekanizması var, eğer cache de varsa ordan yüklüyor datayı. <br>
    
     
# Migration işlemi 
-----------------------
-> EA.BlogProject.Data  klasörünü cmd de açtıktan sonra <br>
-> dotnet ef --startup-project ../EA.BlogProject.WebUI migrations add InitialCreate <br>
-> dotnet ef --startup-project ../EA.BlogProject.WebUI database update <br>
