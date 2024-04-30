using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.MasterService.Models
{
    public class Employee : BaseModel
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}

