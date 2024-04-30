using FluentValidation;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.EmployeeType.Validators;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.VehicleType.Validators;
using System.Net;

namespace Parking.WebAPI.MasterService.BusinessLayer
{
    public class VehicleEntryBL: IVehicleEntryBL
    {
        private readonly IVehicleEntryDAL _vehicleentryDAL;

        public VehicleEntryBL(IVehicleEntryDAL vehicleentryDAL)
        {
            _vehicleentryDAL = vehicleentryDAL;
        }
        public async Task<VehicleEntry> CreateVehicleInEntryAsync(VehicleInEntryRequest request)
        {
            var validator = new VehicleInEntryRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);

            return await _vehicleentryDAL.CreateVehicleInEntryAsync(request);
        }

        public async Task<VehicleEntryDTO> CreateVehicleOutEntryAsync(long id, VehicleOutEntryRequest request)
        {
            var validator = new VehicleOutEntryRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);
            return await _vehicleentryDAL.CreateVehicleOutEntryAsync(id,request);
        }

        public async Task<IEnumerable<VehicleStatusCountDTO>> GetVehicleEntryCountAsync(DateTime selectedDate) => await _vehicleentryDAL.GetVehicleEntryCountAsync(selectedDate);
        public async Task<IEnumerable<VehicleEntryDTO>> GetVehicleEntryAsync(DateTime selectedDate) => await _vehicleentryDAL.GetVehicleEntryAsync(selectedDate);
    }
}

