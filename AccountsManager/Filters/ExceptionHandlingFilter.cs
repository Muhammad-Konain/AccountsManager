using AccountsManager.ApplicationModels.V1.ErrorModels;
using AccountsManager.ApplicationModels.V1.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountsManager.API.Filters
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionResult = new ErrorModel
            {
                ErrorMessage = context.Exception.Message
            };

            context.Result = context.Exception switch
            {
                EntityNotFoundExcetption => new NotFoundObjectResult(exceptionResult),
                InvalidVoucherBalanceException => new BadRequestObjectResult(exceptionResult),
                ArgumentException=> new UnprocessableEntityObjectResult(exceptionResult),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
