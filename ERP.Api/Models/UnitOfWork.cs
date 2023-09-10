using ERP.Api.Interface;

namespace ERP.Api.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
                           IMasterEntryRepository master
                          )
        {

            MasterEntry = master;
        }

        public IMasterEntryRepository MasterEntry { get; set; }
    }
}
