using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERPConnect.Web.Filter
{
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actionName = context.RouteData.Values["action"]?.ToString();

            var authController = new Controllers.AuthController();

            var hasFirstTimeLoginClaim = context.HttpContext.User.HasClaim("FirstTimeLogin", "True");

            if (hasFirstTimeLoginClaim && context.HttpContext.User.Identity.IsAuthenticated && !actionName.Equals("Logout", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = authController.FirstTimePasswordChange();
                return;
            }
        }
    }
}
