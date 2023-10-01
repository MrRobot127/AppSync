using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERPConnect.Web.Controllers
{
    public class ErrorController : Controller
    {        

        [Route("Error/{statusCode}")]

        public IActionResult HttpStatusCodeHandler(int statusCode)
        {            
            switch (statusCode)
            {
                case 401:
                    ViewBag.ErrorMessage = "Unauthorized: You are not authorized to access this resource.";
                    break;

                case 403:
                    ViewBag.ErrorMessage = "Forbidden: Access to this resource is denied.";
                    break;

                case 404:
                    ViewBag.ErrorMessage = "Not Found: The requested resource could not be found.";
                    break;

                case 500:
                    ViewBag.ErrorMessage = "Internal Server Error: An unexpected error occurred.";
                    break;

                case 502:
                    ViewBag.ErrorMessage = "Bad Gateway: The server received an invalid response from an upstream server.";
                    break;

                case 503:
                    ViewBag.ErrorMessage = "Service Unavailable: The server is temporarily unable to handle the request.";
                    break;

                default:
                    ViewBag.ErrorMessage = "An error occurred while processing your request.";
                    break;
            }

            return View("NotFound");
        }
    }
}
