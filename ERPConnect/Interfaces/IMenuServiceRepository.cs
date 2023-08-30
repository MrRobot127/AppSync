using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Interfaces
{
    public interface IMenuServiceRepository
    {
        List<MenuItem> GetMenuItems();
    }
}
