namespace ERPConnect.Web.Middlewares
{
    public class FirstTimeLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public FirstTimeLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == true && context.User.HasClaim("FirstTimeLogin", "True"))
            {
                var actionName = context.GetRouteData().Values["action"]?.ToString();

                if (actionName != null && !string.Equals(actionName, "Logout", StringComparison.OrdinalIgnoreCase))
                {
                    var controllerName = context.GetRouteData().Values["controller"]?.ToString();

                    if (controllerName != null && string.Equals(controllerName, "FirstTimeLogin", StringComparison.OrdinalIgnoreCase))
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