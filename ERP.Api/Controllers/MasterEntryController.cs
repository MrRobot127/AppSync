using ERP.Api.Interface;
using ERP.Api.Models;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers
{
    public class MasterEntryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public MasterEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetCompanyGroup()
        //{


        //    try
        //    {
        //        var data = await _unitOfWork.MasterEntry.GetCompanyGroup();
        //        return Ok(data);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok("ss");
        //    }

        //}

        [HttpGet]
        public async Task<ApiResponse<List<CompanyGroup>>> GetCompanyGroup()
        {
            var apiResponse = new ApiResponse<List<CompanyGroup>>();

            try
            {
                var data = await _unitOfWork.MasterEntry.GetCompanyGroup();
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            return apiResponse;
        }
    }
}
