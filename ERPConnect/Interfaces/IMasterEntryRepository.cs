using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Interfaces
{
    public interface IMasterEntryRepository
    {
        public List<CompanyGroup> GetCompanyGroup();
        CompanyGroup AddCompanyGroup(CompanyGroup companyGroup);

        Company AddCompany(Company company);

        //SubCompany AddSubCompany(SubCompany company);

    }
}
