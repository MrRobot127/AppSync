using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Interfaces
{
    public interface IMasterEntryRepository
    {
        Task<List<CompanyGroup>> GetCompanyGroup();
        Task<List<Company>> GetCompany();
        Task<CompanyGroup> UpdateCompanyGroup(CompanyGroup updatedCompanyGroup);
        Task<CompanyGroup> AddCompanyGroup(CompanyGroup newcompanyGroup);
        
    }
}
