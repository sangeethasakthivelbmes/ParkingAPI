using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.MasterService.DTOs;

namespace Parking.WebAPI.EmployeeType.Validators
{
    public class EmployeeCreationRequestValidator : AbstractValidator<EmployeeCreationRequest>
    {
        public EmployeeCreationRequestValidator()
        {
            var dynamicValues = new Dictionary<string, string>();

            dynamicValues["{fieldName}"] = "Name";
            dynamicValues["{maxLength}"] = "150";
            RuleFor(x => x.Name).NotEmpty()
                .WithErrorCode("ES_COMMON_001")
                .WithMessage(Resources.EmployeeService.EmployeeService.ES_COMMON_001.Replaces(dynamicValues))
                .Length(2, 150)
                .WithErrorCode("ES_COMMON_002")
                .WithMessage(Resources.EmployeeService.EmployeeService.ES_COMMON_002.Replaces(dynamicValues));


            dynamicValues["{fieldName}"] = "Phone";
            dynamicValues.Add("{length}", "10");
            RuleFor(x => x.Phone).NotEmpty()
                .WithErrorCode("ES_COMMON_001")
                .WithMessage(Resources.EmployeeService.EmployeeService.ES_COMMON_001.Replaces(dynamicValues))
                .Matches(@"^\d{10}$")
                .WithErrorCode("ES_COMMON_003")
                .WithMessage(Resources.EmployeeService.EmployeeService.ES_COMMON_003.Replaces(dynamicValues));

            

        }
    }
}

