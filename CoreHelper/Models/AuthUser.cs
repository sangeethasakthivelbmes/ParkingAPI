namespace Parking.WebAPI.CoreHelper.Models
{
    public class AuthUser
    {
        public Int64 UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Int64 RoleId { get; set; }
    }
}

