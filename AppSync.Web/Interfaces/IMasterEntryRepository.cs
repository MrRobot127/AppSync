﻿using AppSync.Web.Models.Entity_Tables;
using AppSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppSync.Web.Interfaces
{
    public interface IMasterEntryRepository
    {
        Task<List<CompanyGroup>> GetCompanyGroup();

        Task<Company> GetCompanyById(int companyId);

        Task<CompanyGroup> UpdateCompanyGroup(CompanyGroup updatedCompanyGroup);

        Task<CompanyGroup> AddCompanyGroup(CompanyGroup newcompanyGroup);

        Task DeleteCompanyGroup(int companyGroupId);

        Task<List<Company>> GetCompany();

        Task<CompanyGroup> GetCompanyGroupById(int id);

        Task<Company> UpdateCompany(Company updatedCompanyDetails);

        Task<Company> AddCompany(AddCompanyViewModel newcompany); 

        Task DeleteCompany(int companyId);


    }
}
