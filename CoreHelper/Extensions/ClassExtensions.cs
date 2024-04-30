using FluentValidation;
using System.Net;

namespace Parking.WebAPI.CoreHelper.Extensions
{
    public static class ClassExtensions
    {
        public static void Validate<TObject, TValidator>(this TObject request)
            where TObject : class
            where TValidator : AbstractValidator<TObject>, new()
        {
            var validator = new TValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);
        }
    }
}
