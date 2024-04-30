using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.MasterService.Models
{
    public class HourEntry : BaseModel
    {
        public long vehicleTypeId { get; set; }    
        public decimal MinHours { get; set; }
        public decimal MinRate { get; set; }
        public decimal AdditionalHour { get; set; }
    }
}

