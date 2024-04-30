using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.MasterService.DTOs;

namespace Parking.WebAPI.VehicleType.Validators
{
    public class VehicleInEntryRequestValidator : AbstractValidator<VehicleInEntryRequest>
    {
        public class VehicleInDateRequest
        {
            public DateTime VehicleInDate { get; set; }
        }
        public VehicleInEntryRequestValidator()
        {
            var datePattern = "^\\d{4}-\\d{2}-\\d{2}$";
            var timePattern = "^([01][0-9]|2[0-3]):[0-5][0-9]$";

            var dynamicValues = new Dictionary<string, string>();

            dynamicValues["{fieldName}"] = "OwnerFullName";
            dynamicValues["{minLength}"] = "2";
            dynamicValues["{maxLength}"] = "150";
            RuleFor(x => x.OwnerFullName).NotEmpty()
                .WithErrorCode("VIS_COMMON_001")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_001.Replaces(dynamicValues))
                .Length(2, 150)
                .WithErrorCode("VIS_COMMON_002")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_002.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "VehicleNumber";
            dynamicValues["{pattern}"] = "XX99XX9999";
            dynamicValues["{length}"] = "10";
            RuleFor(x => x.VehicleNumber).NotEmpty()
                .WithErrorCode("VIS_COMMON_001")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_001.Replaces(dynamicValues))
                .Matches("^[A-Z]{2}\\d{2}[A-Z]{1,2}\\d{1,4}$")
                .WithErrorCode("VIS_COMMON_003")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_003.Replaces(dynamicValues));


            dynamicValues["{fieldName}"] = "VehicleInDate";
            dynamicValues["{pattern}"] = datePattern;
            dynamicValues["{length}"] = "10";
            RuleFor(x => x.VehicleInDate.ToString()).NotEmpty()
                .WithErrorCode("VIS_COMMON_001")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_001.Replaces(dynamicValues));


            dynamicValues["{fieldName}"] = "VehicleInTime";
            dynamicValues["{pattern}"] = timePattern;
            dynamicValues["{length}"] = "5";
            RuleFor(x => x.VehicleInTime.ToString()).NotEmpty()
                .WithErrorCode("VIS_COMMON_001")
                .WithMessage(Resources.VehicleInEntryService.VehicleInEntryService.VIS_COMMON_001.Replaces(dynamicValues));
        }
    }
}

