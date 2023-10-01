namespace ERPConnect.Web.Middlewares
{
    public class FirstTimeLoginMiddleware
    {
        private readonly RequestDelegate _next;

        string[] excludedActions = { "Logout", "HttpStatusCodeHandler" };
        string[] allowedControllers = { "FirstTimeLogin" };

        public FirstTimeLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == true && context.User.HasClaim("FirstTimeLogin", "True"))
            {
                var actionName = context.GetRouteData().Values["action"]?.ToString();

                if (actionName != null && !excludedActions.Contains(actionName, StringComparer.OrdinalIgnoreCase))
                {
                    var controllerName = context.GetRouteData().Values["controller"]?.ToString();

                    if (controllerName != null && allowedControllers.Contains(controllerName, StringComparer.OrdinalIgnoreCase))
                    {
                        await _next(context);
                        return;
                    }

                    context.Response.Redirect("/FirstTimeLogin/Index");
                    return;
                }
            }
            await _next(context);
        }
    }
}