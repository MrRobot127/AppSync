namespace ERP.Api.Interface
{
    public interface IUnitOfWork
    {
        public IMasterEntryRepository MasterEntry { get; set; }
    }
}
