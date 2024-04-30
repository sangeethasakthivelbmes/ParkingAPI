using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.UserService.DataLayer.Interfaces
{
    public interface IAuthDAL
    {
        public Task<User> RegisterUserAsync(UserRegistrationRequest user);
        public Task<User> GetUserAsync(string phone); 
        public Task<bool> CheckOTPAsync(string phone,string OTP);
        public Task<Transaction> SaveOTPAsync(SaveOTPRequest OTP);
        Task<bool> IsPhoneNumberExistsAsync(string phone, string excludePhone = "");
    }
}

