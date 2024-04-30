using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.MasterService.DTOs
{
    public class VehicleTypeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
