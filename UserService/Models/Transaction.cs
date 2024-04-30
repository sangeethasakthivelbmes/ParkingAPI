using Parking.WebAPI.CoreHelper.Model;

namespace Parking.WebAPI.UserService.Models
{
    public class Transaction : BaseModel
    {        
        public string Phone { get; set; }
        public string OTP { get; set; }
    }
}
