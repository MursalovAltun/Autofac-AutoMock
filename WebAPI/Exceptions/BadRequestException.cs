using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using WebAPI.Extensions;

namespace WebAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public ErrorCodes ErrorCode { get; }
        private BadRequestErrorResponse _response;
        private string _data;
        private string _code;

        public BadRequestException(ErrorCodes errorCode, string data = null)
        {
            ErrorCode = errorCode;
            _code = errorCode.ToStringName();
            _data = data;
        }

        public BadRequestException(BadRequestErrorResponse response)
        {
            _response = response;
        }

        public BadRequestErrorResponse Response => _response ?? new BadRequestErrorResponse
        {
            Code = _code,
            Data = _data
        };
    }

    public class BadRequestPropertyError
    {
        public string Code { get; set; }
        public string PropertyName { get; set; }

        public BadRequestPropertyError()
        {
        }

        public BadRequestPropertyError(ValidationFailure validationFailure, string propertyName = null)
        {
            Code = validationFailure.ErrorCode;
            PropertyName = propertyName ??
                           validationFailure.PropertyName.First().ToString().ToLower() +
                           validationFailure.PropertyName.Substring(1);
        }
        
        public BadRequestPropertyError(string propertyName, ErrorCodes errorCode)
        {
            Code = errorCode.ToStringName();
            PropertyName = propertyName;
        }
    }

    public class BadRequestErrorResponse
    {
        public BadRequestErrorResponse()
        {
        }

        public BadRequestErrorResponse(List<BadRequestPropertyError> propertyErrors)
        {
            PropertyErrors = propertyErrors;
        }

        public BadRequestErrorResponse(IList<ValidationFailure> errors)
        {
            PropertyErrors = errors
                .Where(e => !string.IsNullOrEmpty(e.PropertyName))
                .Select(e => new BadRequestPropertyError(e));
        }

        public string Data { get; set; }
        public string Code { get; set; }
        public IEnumerable<BadRequestPropertyError> PropertyErrors { get; set; } = new List<BadRequestPropertyError>();
    }
}