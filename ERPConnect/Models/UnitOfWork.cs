using ERPConnect.Web.Interfaces;

namespace ERPConnect.Web.Models
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(
                            IMenuServiceRepository menuService,
                            IMasterEntryRepository master
                        )
        {
            MenuService = menuService;
            MasterEntry = master;
        }

        public IMenuServiceRepository MenuService { get; set; }
        public IMasterEntryRepository MasterEntry { get; set; }
    }
}
