using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.UserService.DTOs;

namespace Parking.WebAPI.UserService.Validators
{
    public class UserConfirmLoginRequestValidator : AbstractValidator<UserConfirmLoginRequest>
    {
        public UserConfirmLoginRequestValidator()
        {
            var dynamicValues = new Dictionary<string, string>();
            dynamicValues["{fieldName}"] = "Phone number";
            dynamicValues["{length}"] = "10";
            RuleFor(x => x.Phone).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .Matches(@"^\d{10}$")
                .WithErrorCode("US_COMMON_003")
                .WithMessage(Resources.UserService.Common.US_COMMON_003.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "OTP";
            dynamicValues["{length}"] = "6";
            RuleFor(x => x.OTP).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .Matches(@"^\d{6}$")
                .WithErrorCode("US_COMMON_003")
                .WithMessage(Resources.UserService.Common.US_COMMON_003.Replaces(dynamicValues));
            
        }
    }
}

