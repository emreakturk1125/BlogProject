using Microsoft.AspNetCore.Mvc;

namespace EA.BlogProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
