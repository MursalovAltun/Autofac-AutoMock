using System.Linq;
using Autofac.AttributeExtensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Exceptions
{
    [InstancePerLifetimeScope(As = new[] {typeof(IValidatorInterceptor)})]
    public class ValidationInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext,
            ValidationResult result)
        {
            if (result.Errors.Any())
            {
                throw new FluentValidationException(result.Errors);
            }

            return result;
        }
    }
}