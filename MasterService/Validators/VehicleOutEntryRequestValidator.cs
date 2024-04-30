using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.MasterService.DTOs;

namespace Parking.WebAPI.VehicleType.Validators
{
    public class VehicleOutEntryRequestValidator : AbstractValidator<VehicleOutEntryRequest>
    {
        public class VehicleInDateRequest
        {
            public DateTime VehicleInDate { get; set; }
        }
        public VehicleOutEntryRequestValidator()
        {
            var datePattern = "^\\d{4}-\\d{2}-\\d{2}$";
            var timePattern = "^([01][0-9]|2[0-3]):[0-5][0-9]$";

            var dynamicValues = new Dictionary<string, string>();

            dynamicValues["{fieldName}"] = "OwnerFullName";
            dynamicValues["{minLength}"] = "2";
            dynamicValues["{maxLength}"] = "150";
            RuleFor(x => x.OwnerFullName).NotEmpty()
                .WithErrorCode("VOS_COMMON_001")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_001.Replaces(dynamicValues))
                .Length(2, 150)
                .WithErrorCode("VOS_COMMON_002")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_002.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "VehicleNumber";
            dynamicValues["{pattern}"] = "XX99XX9999";
            dynamicValues["{length}"] = "10";
            RuleFor(x => x.VehicleNumber).NotEmpty()
                .WithErrorCode("VOS_COMMON_001")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_001.Replaces(dynamicValues))
                .Matches("^[A-Z]{2}\\d{2}[A-Z]{1,2}\\d{1,4}$")
                .WithErrorCode("VOS_COMMON_003")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_003.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "VehicleOutDate";
            dynamicValues["{pattern}"] = datePattern;
            dynamicValues["{length}"] = "10";
            RuleFor(x => x.VehicleOutDate.ToString()).NotEmpty()
                .WithErrorCode("VOS_COMMON_001")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_001.Replaces(dynamicValues));


            dynamicValues["{fieldName}"] = "VehicleOutTime";
            dynamicValues["{pattern}"] = timePattern;
            dynamicValues["{length}"] = "5";
            RuleFor(x => x.VehicleOutTime.ToString()).NotEmpty()
                .WithErrorCode("VOS_COMMON_001")
                .WithMessage(Resources.VehicleOutEntryService.VehicleOutEntryService.VOS_COMMON_001.Replaces(dynamicValues));
        }
    }
}

