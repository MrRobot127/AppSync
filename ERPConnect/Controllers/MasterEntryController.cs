using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using ERPConnect.Web.Models;

namespace ERPConnect.Web.Controllers
{
    public class MasterEntryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public MasterEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddNewCompanyGroup(CompanyGroup companyGroup)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.MasterEntry.AddCompanyGroup(companyGroup);

                return RedirectToAction("details", new { id = companyGroup.Id });
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MasterEntry.AddCompany(company);
                return RedirectToAction("details", new { id = company.Id });
            }

            return View();

        }
    }
}
