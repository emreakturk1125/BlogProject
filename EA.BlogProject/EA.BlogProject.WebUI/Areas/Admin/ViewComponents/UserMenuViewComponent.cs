using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using EA.BlogProject.Entities.Concrete; 
using EA.BlogProject.WebUI.Areas.Admin.Models;

namespace EA.BlogProject.WebUI.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
                return Content("Kullanıcı bulunamadı");

            if (roles == null)
                return Content("Roller bulunamadı");

            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}
