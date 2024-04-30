using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.BusinessLayer.Interfaces
{
    public interface IHourEntryBL
    {
        public Task<HourEntry> CreateHourSettingAsync(HourEntryRequest request);
    }
}

