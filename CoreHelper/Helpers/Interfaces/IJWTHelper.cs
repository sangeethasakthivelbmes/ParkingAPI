using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Helpers.Interfaces
{
    public interface IJWTHelper
    {
        string GenerateToken(AuthUser user);
        public AuthUser ValidateToken(string token);
    }
}

