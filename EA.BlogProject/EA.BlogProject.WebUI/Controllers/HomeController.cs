using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Entities.Dtos;
using EA.BlogProject.Services.Abstract;

namespace EA.BlogProject.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;

        public HomeController(IArticleService articleService,IOptions<AboutUsPageInfo> aboutUsPageInfo)
        {
            _articleService = articleService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=5, bool isAscending = false)
        {
            var articlesResult = await (categoryId == null
                ? _articleService.GetAllByPagingAsync(null, currentPage, pageSize,isAscending)
                : _articleService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize,isAscending));
            return View(articlesResult.Data);
        }
        [HttpGet]
        public IActionResult About()
        { 
            return View(_aboutUsPageInfo);
        }
        [HttpGet]
        public IActionResult Contact()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
