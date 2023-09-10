
using ERP.Api.Models;
using ERP.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text.Json;

namespace BlazorERP.Web.Services.Implementation
{
    public class CompanyGroupService : ICompanyGroupService
    {
        private readonly HttpClient httpClient;
        public CompanyGroupService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public IEnumerable<CompanyGroup> GetCompanyGroup()
        {
            var response = httpClient.GetAsync("api/MasterEntry").Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON content into a list of CompanyGroup objects using JsonConvert
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CompanyGroup>>>(jsonContent);

                if (apiResponse.Success)
                {
                    return apiResponse.Result;
                }
                else
                {
                    return Enumerable.Empty<CompanyGroup>();
                }
            }
            else
            {
                return Enumerable.Empty<CompanyGroup>();
            }
        }

    }
}
