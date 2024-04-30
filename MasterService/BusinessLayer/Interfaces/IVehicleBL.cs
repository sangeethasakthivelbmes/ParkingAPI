using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using System.Collections.Generic;

namespace Parking.WebAPI.MasterService.BusinessLayer.Interfaces
{
    public interface IVehicleBL
    {
        public Task<bool> UpdateVehicleTypesAsync(List<VehicleTypeDTO> requests);
        public Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypesAsync();
    }
}

