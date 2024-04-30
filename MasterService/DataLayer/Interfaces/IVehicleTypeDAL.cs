using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer.Interfaces
{
    public interface IVehicleTypeDAL
    {
        public Task<bool> UpdateVehicleTypesAsync(VehicleTypeDTO requests);
        public Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypesAsync();
    }
}

