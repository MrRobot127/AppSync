using BlazorERP.Web.Services;
using ERP.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorERP.Web.Pages
{
    public class CompanyGroupListBase : ComponentBase
    {
        [Inject]
        public ICompanyGroupService CompanyGroupService { get; set; }
        public IEnumerable<CompanyGroup> CompanyGroupssss { get; set; }

        private bool isInitialized = false;

        protected override void OnInitialized()
        {

            if (!isInitialized)
            {
                CompanyGroupssss = CompanyGroupService.GetCompanyGroup();
                isInitialized = true;
            }
        }
    }
}
