using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Models.Repository
{
    public class MenuServiceRepository : IMenuServiceRepository
    {
        private readonly AppDbContext _dbContext;

        public MenuServiceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MenuItem> GetMenuItems()
        {
            var menuItems = _dbContext.MenuItems.ToList();

            return menuItems;
        }

    }

}
