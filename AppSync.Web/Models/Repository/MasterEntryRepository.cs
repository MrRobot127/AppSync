﻿using AppSync.Web.Interfaces;
using AppSync.Web.Models.Context;
using AppSync.Web.Models.Entity_Tables;
using AppSync.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppSync.Web.Models.Repository
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

        public async Task<CompanyGroup> GetCompanyGroupById(int id)
        {
            try
            {
                var companyGroup = await _dbContext.CompanyGroups.FirstOrDefaultAsync(c => c.Id == id);
                return companyGroup;
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

        public async Task<Company> UpdateCompany(Company updatedCompanyDetails)
        {
            var existingCompany = await _dbContext.Companies.FindAsync(updatedCompanyDetails.Id);

            if (existingCompany == null)
            {
                throw new Exception("Company not found");
            }

            try
            {
                existingCompany.Name = updatedCompanyDetails.Name;
                existingCompany.Address1 = updatedCompanyDetails.Address1;
                existingCompany.Address2 = updatedCompanyDetails.Address2;
                existingCompany.KeyPerson = updatedCompanyDetails.KeyPerson;
                existingCompany.InvolvingIndustry = updatedCompanyDetails.InvolvingIndustry;
                existingCompany.PhoneNo = updatedCompanyDetails.PhoneNo;
                existingCompany.FaxNo = updatedCompanyDetails.FaxNo;
                existingCompany.Email = updatedCompanyDetails.Email;
                existingCompany.Pfno = updatedCompanyDetails.Pfno;
                existingCompany.Esino = updatedCompanyDetails.Esino;
                existingCompany.HeadOffice = updatedCompanyDetails.HeadOffice;
                existingCompany.PanNo = updatedCompanyDetails.PanNo;
                existingCompany.RegNo = updatedCompanyDetails.RegNo;
                existingCompany.KeyPersonAddress = updatedCompanyDetails.KeyPersonAddress;
                existingCompany.KeyPersonPhNo = updatedCompanyDetails.KeyPersonPhNo;
                existingCompany.KeyPersonDob = updatedCompanyDetails.KeyPersonDob;
                existingCompany.KeyDesignation = updatedCompanyDetails.KeyDesignation;
                existingCompany.RegistrationDate = updatedCompanyDetails.RegistrationDate;
                existingCompany.CreatedBy = updatedCompanyDetails.CreatedBy;
                existingCompany.CreatedOn = updatedCompanyDetails.CreatedOn;
                existingCompany.UpdatedBy = 1;
                existingCompany.UpdatedOn = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return existingCompany;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Company> AddCompany(AddCompanyViewModel newCompany)
        {
            try
            {

                var entityEntry = await _dbContext.Companies.AddAsync(new Company
                {
                    CompanyGroupId = newCompany.CompanyGroupId,
                    Name = newCompany.CompanyName,
                    Address1 = newCompany.Address1,
                    Address2 = newCompany.Address2,
                    KeyPerson = newCompany.KeyPerson,
                    InvolvingIndustry = newCompany.InvolvingIndustry,
                    PhoneNo = newCompany.PhoneNo,
                    FaxNo = newCompany.FaxNo,
                    Email = newCompany.Email,
                    Pfno = newCompany.Pfno,
                    Esino = newCompany.Esino,
                    HeadOffice = newCompany.HeadOffice,
                    PanNo = newCompany.PanNo,
                    RegNo = newCompany.RegNo,
                    KeyPersonAddress = newCompany.KeyPersonAddress,
                    KeyPersonPhNo = newCompany.KeyPersonPhNo,
                    KeyPersonDob = newCompany.KeyPersonDob,
                    KeyDesignation = newCompany.KeyDesignation,
                    RegistrationDate = newCompany.RegistrationDate,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now

                });

                await _dbContext.SaveChangesAsync();

                return entityEntry.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCompany(int companyId)
        {
            try
            {
                var company = await _dbContext.Companies.FindAsync(companyId);

                if (company != null)
                {
                    _dbContext.Companies.Remove(company);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
