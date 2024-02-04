using AppSync.Web.Models;

namespace AppSync.Web.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            Logger.Instance.Error(ex);
        }
    }
}
