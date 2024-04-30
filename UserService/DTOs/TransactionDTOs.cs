namespace Parking.WebAPI.UserService.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public long parking_id { get; set; }
        public string Phone { get; set; }
        public string OTP { get; set; }
    }

    public class SaveOTPRequest
    {
        public string Phone { get; set; }
        public string OTP { get; set; }
    }
}
