using Parking.WebAPI.CoreHelper.Dapper;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;

namespace Parking.WebAPI.CoreHelper
{
    public static class CoreDependancyInjections
    {
        public static void ConfigureCoreDependancyInjections(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            
            services.AddSingleton<IConfiguration>(config);
            services.AddHttpContextAccessor();
            
            services.AddScoped<IDapperWrapper, DapperWrapper>();
            services.AddScoped<IJWTHelper, JWTHelper>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        }
    }
}

