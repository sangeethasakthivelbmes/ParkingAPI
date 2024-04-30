using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DTOs
{
    public class HourEntryRequest
    {
        public  long VehicleTypeId { get; set; }
        public decimal MinHours { get; set; }
        public decimal MinRate { get; set; }
        public decimal AdditionalHour { get; set; }
    }

    public class HourEntryDTO
    {
        public long Id { get; set; }
        public long VehicleTypeId { get; set; }
        public decimal MinHours { get; set; }
        public decimal MinRate { get; set; }
        public decimal AdditionalHour { get; set; }
    }
}

