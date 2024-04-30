using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
namespace Parking.WebAPI.MasterService.DataLayer.Interfaces
{
    public interface IHourEntryDAL
    {
        public Task<HourEntry> CreateHourSettingAsync(HourEntryRequest request); 
    }
}

