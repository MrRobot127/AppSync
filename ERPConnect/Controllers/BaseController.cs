using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ERPConnect.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Account/AccessDenied.cshtml");
        }

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var menuData = GetMenuData();
        //    ViewBag.MenuData = menuData;

        //    base.OnActionExecuting(filterContext);
        //}

        //private List<MenuItem> GetMenuData()
        //{
        //    var menuData = new List<MenuItem>
        //    {
        //        new MenuItem
        //        {
        //            Title = "Home",
        //            URL = "/home/Index",
        //            Submenu = new List<MenuItem>
        //            {
        //                new MenuItem { Title = "Subitem 1", URL = "/home/index" },
        //                new MenuItem { Title = "Subitem 2", URL = "/home/subitem2" }
        //            }
        //        },
        //        new MenuItem
        //        {
        //            Title = "About",
        //            URL = "/about"
        //        },
        //    };

        //    return menuData;
        //}
    }
}
