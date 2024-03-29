﻿using AppSync.Web.Interfaces;
using AppSync.Web.Models.Entity_Tables;
using AppSync.Web.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using AppSync.Web.Models;
using Newtonsoft.Json;
using AppSync.Web.ViewModels;

namespace AppSync.Web.Controllers
{
    public class MasterEntryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> CompanyGroup()
        {
            var companyGroups = await _unitOfWork.MasterEntry.GetCompanyGroup();

            return View(companyGroups);
        }

        [HttpGet("GetCompanyById/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _unitOfWork.MasterEntry.GetCompanyById(id);

            var jsonResult = new JsonResult(new { success = true, data = company });

            return jsonResult;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompanyGroup(CompanyGroup updatedCompanyGroup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedData = await _unitOfWork.MasterEntry.UpdateCompanyGroup(updatedCompanyGroup);
                    var updatedDataArray = JsonConvert.SerializeObject(updatedData);

                    return Json(new { success = true, data = updatedDataArray });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message });
                }
            }

            // If ModelState is not valid, return validation errors as JSON
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
            return Json(new { success = false, errors });
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyGroup(CompanyGroup newCompanyGrup)
        {
            if (ModelState.IsValid)
            {
                var newCompanyGroup = await _unitOfWork.MasterEntry.AddCompanyGroup(newCompanyGrup);
                return Json(new { success = true, data = JsonConvert.SerializeObject(newCompanyGroup) });

            }
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage));
            return Json(new { success = false, errors });

        }

        [HttpDelete("companygroups/{id}")]
        public async Task<IActionResult> DeleteCompanyGroup(int id)
        {
            var companyGroup = await _unitOfWork.MasterEntry.GetCompanyGroupById(id);

            if (companyGroup == null)
            {
                return NotFound(new { success = false, msg = "CompanyGroup not found!" });
            }

            await _unitOfWork.MasterEntry.DeleteCompanyGroup(id);

            return Ok(new { success = true, msg = "CompanyGroup deleted successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Company()
        {
            var company = await _unitOfWork.MasterEntry.GetCompany();

            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompanyDetails([FromBody] Company updatedCompanyDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedData = await _unitOfWork.MasterEntry.UpdateCompany(updatedCompanyDetails);
                    var updatedDataArray = JsonConvert.SerializeObject(updatedData);

                    return Json(new { success = true, data = updatedDataArray });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message });
                }
            }

            // If ModelState is not valid, return validation errors as JSON
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
            return Json(new { success = false, errors });
        }

        [HttpGet]
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = await _unitOfWork.MasterEntry.AddCompany(model);

                if (company != null)
                {
                    ViewBag.IsCompanyAddedSuccessfull = true;
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet("EditCompany/{companyId}")]
        public IActionResult EditCompany(int companyId)
        {
            return View();
        }

        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _unitOfWork.MasterEntry.GetCompanyById(id);

            if (company == null)
            {
                return NotFound(new { success = false, msg = "Company not found!" });
            }

            await _unitOfWork.MasterEntry.DeleteCompany(id);

            return Ok(new { success = true, msg = "Company deleted successfully" });
        }

    }
}
