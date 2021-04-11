using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Exceptions;
using ExceptionContext = Microsoft.AspNetCore.Mvc.Filters.ExceptionContext;

namespace WebAPI.Filters
{
    public class BadRequestExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FluentValidationException fluentValidationException)
            {
                var errorResponse = new BadRequestErrorResponse(fluentValidationException.Errors);
                SetResult(context, new BadRequestObjectResult(errorResponse));
                ResetException(context);
            }

            if (context.Exception is BadRequestException badRequestException)
            {
                SetResult(context, new BadRequestObjectResult(badRequestException.Response));
                ResetException(context);
            }
        }

        private void ResetException(ExceptionContext context)
        {
            context.Exception = null;
        }

        private void SetResult(ExceptionContext context, IActionResult result)
        {
            context.Result = result;
        }
    }
}