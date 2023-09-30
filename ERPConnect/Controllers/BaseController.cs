using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ERPConnect.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Administration/AccessDenied.cshtml");
        }
    }
}
