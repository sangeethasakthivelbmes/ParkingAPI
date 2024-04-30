using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer
{
    public class HourEntryDAL : IHourEntryDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IAuthenticatedUser _authenticatedUser;

        public HourEntryDAL(IDapperWrapper dapperWrapper, IAuthenticatedUser authenticatedUser)
        {
            _dapperWrapper = dapperWrapper;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<HourEntry> CreateHourSettingAsync(HourEntryRequest request)
        {
            var query = @"
                        WITH inserted_hour_entry AS (
                            INSERT INTO hour_entry  (
                                created_by, created_on, updated_by, updated_on, 
                                 vehicle_typeid, min_hours, min_rate,additional_hour
                            )
                            VALUES (
                                @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, @VehicleTypeId, 
                                @MinHours, @MinRate, @AdditionalHour
                            )
                            RETURNING 
                                id,  vehicle_typeid AS VehicleTypeId, min_hours AS MinHours, min_rate AS MinRate, additional_hour AS AdditionalHour
                        )
                        SELECT
                            id AS Id, VehicleTypeId, MinHours, MinRate, AdditionalHour
                        FROM inserted_hour_entry
                    ";

            var result = await _dapperWrapper.QuerySingleAsync<HourEntry>(
                query,
                new
                {
                    CreatedBy = 0,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    VehicleTypeId = request.VehicleTypeId,
                    MinHours = request.MinHours,
                    MinRate = request.MinRate,
                    AdditionalHour = request.AdditionalHour,
                }
            );
            return result;
        }
    }
}

