using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.UserService.BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public Task<UserDTO> GetAuthenticatedUserAsync();
        public Task<bool> UpdateUserAsync(long id, UserUpdationRequest user);
    }
}

