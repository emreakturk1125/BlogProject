using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Entities.Dtos;
using EA.BlogProject.Services.Abstract;
using NToastNotify;
using EA.BlogProject.Shared.Utilities.Helpers.Abstract;

namespace EA.BlogProject.Mvc.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly AboutUsPageInfo _aboutUsPageInfo; 
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;

        public HomeController(IArticleService articleService, IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter)
        {
            _articleService = articleService;
            _mailService = mailService;
            _toastNotification = toastNotification;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter; 
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [Route("index")]
        [Route("anasayfa")]
        [Route("")] 
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=5, bool isAscending = false)
        {
            var articlesResult = await (categoryId == null
                ? _articleService.GetAllByPagingAsync(null, currentPage, pageSize,isAscending)
                : _articleService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize,isAscending));
            return View(articlesResult.Data);
        }
        [Route("hakkimizda")]
        [Route("hakkinda")]
        [HttpGet]
        public IActionResult About()
        { 
            return View(_aboutUsPageInfo);
        }

        [Route("iletisim")] 
        [HttpGet]
        public IActionResult Contact()
        { 
            return View();
        }

        [Route("iletisim")] 
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View();

            }
            return View(emailSendDto);
        }

        [Route("sayfabulunamadi")] 
        [HttpGet]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
