using AutoMapper;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Mvc.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EA.BlogProject.WebUI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper; 
        }

        protected UserManager<User> UserManager { get; }    
       protected IMapper Mapper { get; }
       protected IImageHelper ImageHelper { get; }
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;
    }
}
