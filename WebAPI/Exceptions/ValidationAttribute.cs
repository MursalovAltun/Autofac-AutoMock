using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Exceptions
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                throw new BadHttpRequestException("actionContext.ModelState");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}