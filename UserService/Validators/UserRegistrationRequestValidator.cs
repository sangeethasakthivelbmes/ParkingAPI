using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.UserService.DTOs;

namespace Parking.WebAPI.UserService.Validators
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
            {
            var dynamicValues = new Dictionary<string, string>
            {
                { "{fieldName}", "Name" },
                { "{minLength}", "2" },
                { "{maxLength}", "75" }
            };

            RuleFor(x => x.Name).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .Length(2, 75)
                .WithErrorCode("US_COMMON_002")
                .WithMessage(Resources.UserService.Common.US_COMMON_002.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "Phone";
            dynamicValues.Add("{length}", "10");
            RuleFor(x => x.Phone).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .Matches(@"^\d{10}$")
                .WithErrorCode("US_COMMON_003")
                .WithMessage(Resources.UserService.Common.US_COMMON_003.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "Email";
            dynamicValues["{maxLength}"] = "150";
            RuleFor(x => x.Email).NotEmpty()
                .WithErrorCode("US_COMMON_001")
                .WithMessage(Resources.UserService.Common.US_COMMON_001.Replaces(dynamicValues))
                .EmailAddress()
                .WithErrorCode("US_COMMON_004")
                .WithMessage(Resources.UserService.Common.US_COMMON_004.Replaces(dynamicValues))
                .Length(2, 150);
        }
    }
}

