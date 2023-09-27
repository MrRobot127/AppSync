using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERPConnect.Web.Filter
{
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authController = new Controllers.AuthController();

            var hasFirstTimeLoginClaim = context.HttpContext.User.HasClaim("FirstTimeLogin", "True");

            if (hasFirstTimeLoginClaim)
            {
                context.Result = authController.FirstTimePasswordChange();                
                return;
            }
        }
    }
}
