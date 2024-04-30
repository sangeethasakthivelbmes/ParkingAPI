using Parking.WebAPI.EmployeeType.Validators;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.UserService.Validators;
using System.Net;

using Parking.WebAPI.CoreHelper.Extensions;
using static System.Net.WebRequestMethods;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.HourSettingType.Validators;

namespace Parking.WebAPI.MasterService.BusinessLayer
{
    public class HourEntryBL : IHourEntryBL
    {
        private readonly IHourEntryDAL _hoursettingDAL;

        public HourEntryBL(IHourEntryDAL hoursettingDAL)
        {
            _hoursettingDAL = hoursettingDAL;
        }

        public async Task<HourEntry> CreateHourSettingAsync(HourEntryRequest request)
        {
            var validator = new HourSettingRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);

            return await _hoursettingDAL.CreateHourSettingAsync(request);
        }
    }
}

