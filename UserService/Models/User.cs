using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.UserService.Models
{
    public class User : BaseModel
    {
        public bool IsActive { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StandName { get; set; }
        public string Addresses { get; set; }
    }
}

