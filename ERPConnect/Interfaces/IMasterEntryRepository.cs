using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Interfaces
{
    public interface IMasterEntryRepository
    {
        Task<List<CompanyGroup>> GetCompanyGroup();
        Task<Company> GetCompanyById(int companyId);
        Task<List<Company>> GetCompany();
        Task<CompanyGroup> UpdateCompanyGroup(CompanyGroup updatedCompanyGroup);
        Task<CompanyGroup> AddCompanyGroup(CompanyGroup newcompanyGroup);
        Task<CompanyGroup> GetCompanyGroupById(int id);

        Task DeleteCompanyGroup(int id);

    }
}
