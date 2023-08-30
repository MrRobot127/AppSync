using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.Models.Repository;
using Microsoft.AspNetCore.Mvc;

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
                CompanyGroup tblGroupOfCompany = new CompanyGroup
                {
                    GroupName = companyGroup.GroupName
                };

                _unitOfWork.MasterEntry.AddCompanyGroup(companyGroup);

                return RedirectToAction("details", new { id = companyGroup.Id });
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if(ModelState.IsValid)
            {
                Company tblCompany = new Company
                {
                    
                };
            }

            return View();

        }
    }
}
