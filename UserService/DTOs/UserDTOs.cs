namespace Parking.WebAPI.UserService.DTOs
{
    public class UserRegistrationRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class UserUpdationRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StandName { get; set; }
        public string Addresses { get; set; }
    }

    public class UserLoginRequest
    {
        public string Phone { get; set; }
    }

    public class UserConfirmLoginRequest
    {
        public string Phone { get; set; }
        public string OTP { get; set; }
    }

    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
        public string Note { get; set; }
        public string StandName { get; set; }
        public string Addresses { get; set; }
    }
}

