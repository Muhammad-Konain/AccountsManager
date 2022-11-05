using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountsManager.API.Filters
{
    public class LoggingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();   
        }
    }
}
