using ERP.Models;

namespace ERP.Api.Interface
{
    public interface IMasterEntryRepository
    {
        Task<List<CompanyGroup>> GetCompanyGroup();
    }
}
