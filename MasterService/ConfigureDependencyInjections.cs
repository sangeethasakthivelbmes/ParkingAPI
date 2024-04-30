using Parking.WebAPI.MasterService.BusinessLayer;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;

namespace Parking.WebAPI.MasterService
{
    public static class CongigureDependancyInjections
    {
        public static void CongigureVSDependancyInjections(this IServiceCollection services)
        {
            services.AddScoped<IVehicleTypeDAL, VehicleTypeDAL>();
            services.AddScoped<IVehicleBL, VehicleBL>();
            services.AddScoped<IVehicleEntryDAL, VehicleEntryDAL>();
            services.AddScoped<IVehicleEntryBL, VehicleEntryBL>();
            services.AddScoped<IEmployeeDAL, EmployeeDAL>();
            services.AddScoped<IEmployeeBL, EmployeeBL>();
            services.AddScoped<IHourEntryDAL, HourEntryDAL>();
            services.AddScoped<IHourEntryBL, HourEntryBL>();
        }
    }
}

