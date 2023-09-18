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

        public async Task<List<CompanyGroup>> GetCompanyGroup()
        {
            try
            {
                var lstcompanyGroup = await _dbContext.CompanyGroups.ToListAsync();

                return lstcompanyGroup;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Company> GetCompanyById(int companyId)
        {
            try
            {
                var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

                return company;
            }
            catch
            {
                throw;
            }
        }

        

        public async Task<List<Company>> GetCompany()
        {
            try
            {
                var lstCompany = await _dbContext.Companies.ToListAsync();

                return lstCompany;
            }
            catch
            {
                throw;
            }
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

                    existingGroup.ModifiedBy = 1; //will change once User Functionality added
                    existingGroup.ModifiedOn = DateTime.Now;

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

        public async Task<CompanyGroup> AddCompanyGroup(CompanyGroup newCompanyGrup)
        {
            try
            {
                newCompanyGrup.CreatedBy = 1; //will change once User Functionality added
                newCompanyGrup.CreatedOn = DateTime.Now;

                _dbContext.CompanyGroups.Add(newCompanyGrup);
                await _dbContext.SaveChangesAsync();

                return newCompanyGrup;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCompanyGroup(int id)
        {
            try
            {
                var companyGroup = await _dbContext.CompanyGroups.FindAsync(id);

                if (companyGroup != null)
                {
                    _dbContext.CompanyGroups.Remove(companyGroup);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyGroup> GetCompanyGroupById(int id)
        {
            try
            {
                var companyGroup = await _dbContext.CompanyGroups.FirstOrDefaultAsync(c => c.Id == id);
                return companyGroup;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
