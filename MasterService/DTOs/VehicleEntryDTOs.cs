using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.UserService.DTOs;

namespace Parking.WebAPI.MasterService.DTOs
{
    public class VehicleEntryDTO
    {
        public long Id { get; set; }
        public long VehicleTypeId { get; set; }
        public VehicleTypeDTO VehicleTypes { get; set; }
        public  string OwnerFullName { get; set; }
        public  string VehicleNumber { get; set; }
        public DateTime VehicleInDate { get; set; }
        public DateTime VehicleOutDate { get; set; }
        public TimeSpan VehicleInTime { get; set; }
        public TimeSpan VehicleOutTime { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalCost { get; set; }
    }

    public class VehicleInEntryRequest
    {
        public  long VehicleTypeId { get; set; }
        public  string OwnerFullName { get; set; }
        public  string VehicleNumber { get; set; }
        public DateTime VehicleInDate { get; set; }
        public string VehicleInTime { get; set; }

    }

    public class VehicleOutEntryRequest
    {
        public  string OwnerFullName { get; set; }
        public  string VehicleNumber { get; set; }
        public DateTime VehicleOutDate { get; set; }
        public string VehicleOutTime { get; set; }
    }

    public class VehicleStatusCountDTO
    {
        public string VehicleStatus { get; set; }
        public int StatusCount { get; set; }
    }
}
