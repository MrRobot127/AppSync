using AppSync.Web.Models.Entity_Tables;

namespace AppSync.Web.Interfaces
{
    public interface IMenuServiceRepository
    {
        List<MenuItem> GetMenuItems();
    }
}
