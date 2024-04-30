using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.UserService.DTOs;

namespace Parking.WebAPI.UserService.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            var dynamicValues = new Dictionary<string, string>();
            dynamicValues["{fieldName}"] = "Phone number";
            dynamicValues.Add("{length}", "10");
            RuleFor(x => x.Phone).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .Matches(@"^\d{10}$")
                .WithErrorCode("US_COMMON_003")
                .WithMessage(Resources.UserService.Common.US_COMMON_003.Replaces(dynamicValues));
            
        }
    }
}

