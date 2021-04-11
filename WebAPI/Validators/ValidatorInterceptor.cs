using System.Linq;
using Autofac.AttributeExtensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;

namespace WebAPI.Validators
{
    [InstancePerLifetimeScope(As = new[] {typeof(IValidatorInterceptor)})]
    public class ValidationInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext,
            IValidationContext validationContext)
        {
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext,
            IValidationContext commonContext,
            ValidationResult result)
        {
            if (result.Errors.Any())
            {
                throw new FluentValidationException(result.Errors);
            }

            return result;
        }

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