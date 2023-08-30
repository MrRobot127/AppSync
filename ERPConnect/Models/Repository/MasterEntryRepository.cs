using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Entity_Tables;
using Microsoft.EntityFrameworkCore;

namespace ERPConnect.Web.Models.Repository
{
    public class MasterEntryRepository : IMasterEntryRepository
    {
        private readonly AppDbContext _dbContext;
        public MasterEntryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Company AddCompany(Company company)
        {
            var addedCompany = _dbContext.Companies.Add(company).Entity;
            _dbContext.SaveChanges();

            return addedCompany;
        }

        public CompanyGroup AddCompanyGroup(CompanyGroup companyGroup)
        {
            var addedGroup = _dbContext.CompanyGroups.Add(companyGroup).Entity;
            _dbContext.SaveChanges();

            return addedGroup;
        }
    }
}
