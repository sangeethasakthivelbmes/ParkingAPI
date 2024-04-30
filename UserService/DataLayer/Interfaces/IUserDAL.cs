using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.UserService.DataLayer.Interfaces
{
    public interface IUserDAL
    {
        public Task<UserDTO> GetAuthenticatedUserAsync();
        public Task<UserDTO> GetUserAsync(long Id);
        public Task<bool> UpdateUserAsync(long id, UserUpdationRequest user);
    }
}

