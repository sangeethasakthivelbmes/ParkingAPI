using Microsoft.AspNetCore.Http.HttpResults;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer
{
    public class VehicleTypeDAL : IVehicleTypeDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IAuthenticatedUser _authenticatedUser;

        public VehicleTypeDAL(IDapperWrapper dapperWrapper, IAuthenticatedUser authenticatedUser)
        {
            _dapperWrapper = dapperWrapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetVehicleTypesAsync()
        {
            var query = @"
                SELECT
                    id as Id, name as Name,is_active as IsActive
                FROM vehicle_types order by id asc
            ";
            var vehicles = await _dapperWrapper.QueryAsync<VehicleTypeDTO>(query);
            return vehicles;
        }

        public async Task<bool> UpdateVehicleTypesAsync(VehicleTypeDTO request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return false;
            }
            int affectedRows = 0;
            var query = @"
                 WITH updated_vehicle_types  AS (
                    UPDATE vehicle_types
                    SET
                        updated_by = @UpdatedBy,
                        updated_on = @UpdatedOn,
                        is_active = @IsActive       
                    WHERE Id = @Id
                    RETURNING *
                )
                 SELECT * FROM updated_vehicle_types;
            ";


            var updatedRows = await _dapperWrapper.QueryAsync<dynamic>(
                query,
                new
                {
                    Id = request.Id,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    Name = request.Name,
                    IsActive = request.IsActive
                }
            ); affectedRows = updatedRows.Count();
            return affectedRows == 1;
        }
    }
}

