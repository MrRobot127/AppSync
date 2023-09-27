using ERPConnect.Web.Filter;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ERPConnect.Web.Controllers
{
    [AuthorizationFilterAttribute]
    public  class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Administration/AccessDenied.cshtml");
        }
    }
}
