using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.MasterService.Models
{
    public class VehicleEntry : BaseModel
    {
        public Vehicle Vehicle { get; set; }
        public string OwnerFullName { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime? VehicleInDate { get; set; }
        public DateTime? VehicleOutDate { get; set; }
        public TimeSpan VehicleInTime { get; set; }
        public TimeSpan VehicleOutTime { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalCost { get; set; }
    }
}

