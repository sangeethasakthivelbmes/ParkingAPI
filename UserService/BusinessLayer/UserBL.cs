using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.UserService.BusinessLayer.Interfaces;
using Parking.WebAPI.UserService.DataLayer;
using Parking.WebAPI.UserService.DataLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;
using Parking.WebAPI.UserService.Validators;
using System.Net;

namespace Parking.WebAPI.UserService.BusinessLayer
{
    public class UserBL: IUserBL
    {
        private readonly IUserDAL _userDAL;

        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public async Task<UserDTO> GetAuthenticatedUserAsync()
        {
            return await _userDAL.GetAuthenticatedUserAsync();
        }
        public async Task<bool> UpdateUserAsync(long id,UserUpdationRequest user)
        {
            return await _userDAL.UpdateUserAsync(id,user);
        }
    }
}

