using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            try
            {
                var addedCompany = _dbContext.Companies.Add(company).Entity;
                _dbContext.SaveChanges();

                return addedCompany;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyGroup> UpdateCompanyGroup(CompanyGroup updatedCompanyGroup)
        {
            try
            {
                var existingGroup = await _dbContext.CompanyGroups.FindAsync(updatedCompanyGroup.Id);

                if (existingGroup != null)
                {
                    existingGroup.GroupName = updatedCompanyGroup.GroupName;
                    existingGroup.IsActive = updatedCompanyGroup.IsActive;

                    await _dbContext.SaveChangesAsync();
                    return existingGroup;
                }
                else
                {
                    throw new Exception("Group doesn't exist.");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CompanyGroup>> GetCompanyGroup()
        {
            try
            {
                var lstcompanyGroup = await _dbContext.CompanyGroups.Where(group => group.IsActive == true).ToListAsync();

                return lstcompanyGroup;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyGroup> AddCompanyGroup(CompanyGroup newCompanyGrup)
        {
            try
            {
                _dbContext.CompanyGroups.Add(newCompanyGrup);
                await _dbContext.SaveChangesAsync();

                return newCompanyGrup;
            }
            catch
            {
                throw;
            }
        }
    }
}
