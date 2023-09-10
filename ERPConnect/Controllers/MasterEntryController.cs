using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using ERPConnect.Web.Models;
using Newtonsoft.Json;

namespace ERPConnect.Web.Controllers
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
            var companyGroup = await _unitOfWork.MasterEntry.GetCompanyGroup();

            return View(companyGroup);
        }

        [HttpGet]
        public async Task<IActionResult> Company()
        {
            var company = await _unitOfWork.MasterEntry.GetCompany();

            return View(company);
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
                    return Json(new { success = false,msg = ex.Message });
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
                try
                {
                    var newCompanyGroup = await _unitOfWork.MasterEntry.AddCompanyGroup(newCompanyGrup);
                    return Json(new { success = true, data = JsonConvert.SerializeObject(newCompanyGroup) });
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, error = ex.Message });
                }
            }

            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage));
            return Json(new { success = false, errors });

        }
    }
}
