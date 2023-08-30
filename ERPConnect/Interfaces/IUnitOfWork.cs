namespace ERPConnect.Web.Interfaces
{
    public interface IUnitOfWork
    {
        public IMenuServiceRepository MenuService { get; set; }
        public IMasterEntryRepository MasterEntry { get; set; }   
    }
}
