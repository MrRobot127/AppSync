using ERP.Models;
using System.Collections;

namespace BlazorERP.Web.Services
{
    public interface ICompanyGroupService
    {
        IEnumerable<CompanyGroup> GetCompanyGroup();
    }

}
