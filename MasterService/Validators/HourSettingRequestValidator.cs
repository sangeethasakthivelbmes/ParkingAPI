using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.MasterService.DTOs;

namespace Parking.WebAPI.HourSettingType.Validators
{
    public class HourSettingRequestValidator : AbstractValidator<HourEntryRequest>
    {
        public HourSettingRequestValidator()
        {
            var dynamicValues = new Dictionary<string, string>();


            dynamicValues["{fieldName}"] = "MinHours";
            RuleFor(x => x.MinHours)
                .GreaterThan(0) 
                .WithErrorCode("HS_COMMON_001")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_001.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "MinHours";
            dynamicValues["{length}"] = "6";
            RuleFor(x => x.MinHours.ToString())
                .Matches(@"^\d{1,6}(\.\d{1,2})?$")
                .WithErrorCode("HS_COMMON_002")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_002.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "AdditionalHours";
            RuleFor(x => x.AdditionalHour)
                .GreaterThan(0)
                .WithErrorCode("HS_COMMON_001")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_001.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "AdditionalHours";
            dynamicValues["{length}"] = "6";
            RuleFor(x => x.AdditionalHour.ToString())
                .Matches(@"^\d{1,6}(\.\d{1,2})?$")
                .WithErrorCode("HS_COMMON_002")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_002.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "MinimumRate";
            RuleFor(x => x.MinRate)
                .GreaterThan(0)
                .WithErrorCode("HS_COMMON_001")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_001.Replaces(dynamicValues));

            dynamicValues["{fieldName}"] = "MinimumRate";
            dynamicValues["{length}"] = "6";
            RuleFor(x => x.MinRate.ToString())
                .Matches(@"^\d{1,6}(\.\d{1,2})?$")
                .WithErrorCode("HS_COMMON_002")
                .WithMessage(Resources.HourSettingService.HourSettingService.HS_COMMON_002.Replaces(dynamicValues));

        }
    }
}

