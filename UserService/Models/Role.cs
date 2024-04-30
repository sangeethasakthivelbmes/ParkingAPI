using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.UserService.Models
{
    public class Role: BaseModel
    {
        public string Name { get; set; }
        public RoleType RoleType { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }

    public enum  RoleType
    {
        Admin = 1,
        User
    }
}

