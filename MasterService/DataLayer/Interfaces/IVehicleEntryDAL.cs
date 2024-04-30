using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer.Interfaces
{
    public interface IVehicleEntryDAL
    {
        public Task<VehicleEntry> CreateVehicleInEntryAsync(VehicleInEntryRequest request);
        public Task<VehicleEntryDTO> CreateVehicleOutEntryAsync(long id, VehicleOutEntryRequest request);
        public Task<IEnumerable<VehicleStatusCountDTO>> GetVehicleEntryCountAsync(DateTime selectedDate);
        public Task<IEnumerable<VehicleEntryDTO>> GetVehicleEntryAsync(DateTime selectedDate);
    }
}

