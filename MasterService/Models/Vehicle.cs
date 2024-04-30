using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.MasterService.Models
{
    public class Vehicle : BaseModel
    {

        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool IsActive { get; set; }
    }

    public enum VehicleType
    {
        TwoWheeler = 1,
        ThreeWheeler,
        FourWheeler
    }
}

