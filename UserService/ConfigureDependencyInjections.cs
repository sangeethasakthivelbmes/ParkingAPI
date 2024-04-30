using Parking.WebAPI.UserService.BusinessLayer;
using Parking.WebAPI.UserService.BusinessLayer.Interfaces;
using Parking.WebAPI.UserService.DataLayer;
using Parking.WebAPI.UserService.DataLayer.Interfaces;

namespace Parking.WebAPI.UserService
{
    public static class CongigureDependancyInjections
    {
        public static void CongigureUSDependancyInjections(this IServiceCollection services)
        {
            services.AddScoped<IAuthDAL, AuthDAL>();
            services.AddScoped<IAuthBL, AuthBL>();
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IUserBL, UserBL>();
        }        
    }
}

