using Microsoft.AspNetCore.Mvc;

namespace ERPConnect.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult FirstTimePasswordChange()
        {
            return View("~/Views/Account/FirstTimePasswordChange.cshtml");
        }
    }
}
