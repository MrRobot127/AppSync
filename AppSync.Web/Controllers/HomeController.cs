using Microsoft.AspNetCore.Mvc;

namespace AppSync.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
