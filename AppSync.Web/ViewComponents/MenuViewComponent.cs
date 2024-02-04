using AppSync.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppSync.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            var menuItems = _unitOfWork.MenuService.GetMenuItems();
            return View("_Menu", menuItems);
        }
    }
}
