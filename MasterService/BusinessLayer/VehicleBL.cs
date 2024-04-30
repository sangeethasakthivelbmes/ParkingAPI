using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.BusinessLayer
{
    public class VehicleBL : IVehicleBL
    {
        private readonly IVehicleTypeDAL _vehicleDAL;

        public VehicleBL(IVehicleTypeDAL vehicleDAL)
        {
            _vehicleDAL = vehicleDAL;
        }
        public async Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypesAsync() => await _vehicleDAL.GetVehicleTypesAsync();
        public async Task<bool> UpdateVehicleTypesAsync(List<VehicleTypeDTO> requests)
        {
            foreach (var type in requests)
            {
                bool success = await _vehicleDAL.UpdateVehicleTypesAsync(type);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

