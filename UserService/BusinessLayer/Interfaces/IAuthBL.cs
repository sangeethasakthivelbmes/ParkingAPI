using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.UserService.BusinessLayer.Interfaces
{
    public interface IAuthBL
    {
        public Task<User> RegisterUserAsync(UserRegistrationRequest user);         
        public Task<User> AuthenticateUserAsync(UserLoginRequest request);
        public Task<bool> ConfirmLoginAsync(UserConfirmLoginRequest request);
        public Task<Transaction> SaveOTPAsync(SaveOTPRequest OTP);
    }
}

