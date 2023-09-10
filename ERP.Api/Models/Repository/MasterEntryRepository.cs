using ERP.Api.Interface;
using ERP.Api.Models.Context;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api.Models.Repository
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
                var lstcompanyGroup = await _dbContext.CompanyGroup.Where(group => group.IsActive == true).ToListAsync();

                return lstcompanyGroup;
            }
            catch
            {
                throw;
            }
        }
    }
}
