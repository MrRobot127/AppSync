using AppSync.Web.Interfaces;
using AppSync.Web.Models.Context;
using AppSync.Web.Models.Entity_Tables;

namespace AppSync.Web.Models.Repository
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
