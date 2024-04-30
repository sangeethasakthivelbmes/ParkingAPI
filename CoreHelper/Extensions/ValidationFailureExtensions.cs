using System.Globalization;
using System.Net;
using FluentValidation.Results;
using Parking.WebAPI.CoreHelper.Exceptions;
using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Extensions
{
    public static class ValidationFailureExtensions
    {
        public static HttpResponseException CustomException(this List<ValidationFailure> validationFailures, HttpStatusCode statusCode)
        {
            return new HttpResponseException((int)statusCode, validationFailures.ToCustomErrors());
        }

        public static HttpResponseException CustomException(this ValidationFailure validationFailure, HttpStatusCode statusCode)
        {
            return new HttpResponseException((int)statusCode, validationFailure.ToCustomError());
        }

        public static List<CustomError> ToCustomErrors(this List<ValidationFailure> validationFailures)
        {
            return validationFailures.Select(s => s.ToCustomError()).ToList();
        }

        public static CustomError ToCustomError(this ValidationFailure validationFailure)
        {
            return new CustomError
            {
                ErrorCode = validationFailure.ErrorCode,
                Message = validationFailure.ErrorMessage,
                LanguageCode = CultureInfo.CurrentCulture.Name
            };
        }
    }
}

