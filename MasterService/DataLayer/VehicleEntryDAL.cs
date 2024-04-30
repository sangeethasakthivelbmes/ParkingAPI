using Dapper;
using Microsoft.VisualBasic;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.TypeHandlers;
using Parking.WebAPI.MasterService.BusinessLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.MasterService.DataLayer
{
    public class VehicleEntryDAL : IVehicleEntryDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IAuthenticatedUser _authenticatedUser;

        public VehicleEntryDAL(IDapperWrapper dapperWrapper, IAuthenticatedUser authenticatedUser)
        {
            _dapperWrapper = dapperWrapper;
            _authenticatedUser = authenticatedUser;
        }      

        public async Task<VehicleEntry> CreateVehicleInEntryAsync(VehicleInEntryRequest request)
        {
            var vehicleEntry = new VehicleEntry();
            vehicleEntry.Vehicle = new Vehicle { Id= request.VehicleTypeId };
            var query = @"
                 WITH inserted_vehicle_entry  AS (
                    INSERT INTO vehicle_entry  (
                                created_by, created_on, updated_by, updated_on, 
                                 vehicle_typeId, owner_full_name, vehicle_number,in_date,in_time,vehicle_status
                            )
                            VALUES (
                                @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, @VehicleTypeId, 
                                @OwnerFullName, @VehicleNumber, @VehicleInDate,(CAST(@VehicleInTime AS time)),1
                            )
                            RETURNING 
                                vehicle_typeId AS VehicleTypeId, owner_full_name AS OwnerFullName,
                                vehicle_number AS VehicleNumber, in_date AS VehicleInDate,vehicle_status As VehicleStatus                                
                        )
                        SELECT
                            id AS Id, VehicleTypeId,OwnerFullName,VehicleNumber,VehicleInDate,VehicleStatus,
                            json_build_object(
                                'Id', t.id, 
                                'Name', t.name,
                                'CreatedBy', t.created_by,
                                'CreatedOn', t.created_on,
                                'UpdatedBy', t.updated_by,
                                'UpdatedOn', t.updated_on,
                                'IsActive', t.is_active
                            ) as Vehicle
                        FROM inserted_vehicle_entry e
                        JOIN vehicle_types t ON e.VehicleTypeId = t.Id
            ";

            SqlMapper.AddTypeHandler(typeof(Vehicle), new JsonTypeHandler<Vehicle>());
            var result = await _dapperWrapper.QuerySingleAsync<VehicleEntry>(
                query,
                new
                {
                    CreatedBy = 0,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    VehicleTypeId = vehicleEntry.Vehicle.Id,
                    OwnerFullName = request.OwnerFullName,
                    VehicleNumber = request.VehicleNumber,
                    VehicleInDate = request.VehicleInDate,
                    VehicleInTime = request.VehicleInTime
                }
            );
            return vehicleEntry;
        }

        public async Task<VehicleEntryDTO> GetvehicleDetailsByID(long id)
        {
            try
            {
                var query = @"
                SELECT
                    vehicle_typeid as VehicleTypeId,
                    in_date AS VehicleInDate,
                    in_time AS VehicleInTime
                FROM vehicle_entry 
                WHERE id = @id
            ";

            var vehicle = await _dapperWrapper.QuerySingleAsync<VehicleEntryDTO>(query, new { id });

            return vehicle;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving vehicle details: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<VehicleStatusCountDTO>> GetVehicleEntryCountAsync(DateTime selectedDate)
        {
            var query = @"
                WITH PossibleStatuses AS (
                    SELECT 1 AS vehicle_status, 'IN' AS VehicleStatus
                    UNION ALL
                    SELECT 2 AS vehicle_status, 'OUT' AS VehicleStatus
                )

                SELECT 
                    ps.VehicleStatus,
                    COALESCE(data.StatusCount, 0) AS StatusCount
                FROM 
                    PossibleStatuses ps
                LEFT JOIN 
                    (
                        SELECT 
                            CASE 
                                WHEN vehicle_status = 1 THEN 'IN'
                                WHEN vehicle_status = 2 THEN 'OUT'
                            END AS VehicleStatus,
                            COUNT(*) AS StatusCount,
                            vehicle_status
                        FROM 
                            public.vehicle_entry
                        WHERE 
                            vehicle_status IN (1, 2)
                            AND DATE(updated_on) = @selectedDate
                        GROUP BY 
                            vehicle_status
                    ) data 
                ON 
                    ps.vehicle_status = data.vehicle_status
                ORDER BY 
                    ps.vehicle_status 
            ";
            var vehicleentry = await _dapperWrapper.QueryAsync<VehicleStatusCountDTO>(query, new { selectedDate });
            return vehicleentry;
        }

        public async Task<IEnumerable<VehicleEntryDTO>> GetVehicleEntryAsync(DateTime selectedDate)
        {
            var query = @"
                SELECT 
                   id As ID,vehicle_number As VehicleNumber, in_date As VehicleInDate,
				   in_time As VehicleInTime
                FROM 
                    public.vehicle_entry
                WHERE 
                    DATE(Updated_On) = @selectedDate
            ";
            var vehicleentry = await _dapperWrapper.QueryAsync<VehicleEntryDTO>(query, new { selectedDate });
            return vehicleentry;
        }
        public async Task<HourEntryDTO> GetHourRate(long VehicleTypeId)
        {
            try
            {
                var query = @"
                SELECT
                    min_hours AS MinHours,
                    min_rate AS MinRate,
                    additional_hour AS AdditionalHour
                FROM hour_entry 
                WHERE vehicle_typeid = @VehicleTypeId
                ORDER BY Updated_On desc
                LIMIT 1
            ";

                var hourentry = await _dapperWrapper.QuerySingleAsync<HourEntryDTO>(query, new { VehicleTypeId });

                return hourentry;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving vehicle details: {ex.Message}");
                throw;
            }
        }
        public async Task<VehicleEntryDTO> CreateVehicleOutEntryAsync(long id, VehicleOutEntryRequest request)
        {
            var vehicleDetails = await GetvehicleDetailsByID(id);

            var hourRate = await GetHourRate(vehicleDetails.VehicleTypeId);

            double MinimumHours = Convert.ToDouble(hourRate.MinHours);
            double MinimumRate = Convert.ToDouble(hourRate.MinRate);
            double AdditionalHour = Convert.ToDouble(hourRate.AdditionalHour);

            DateTime inDateTime = vehicleDetails.VehicleInDate + vehicleDetails.VehicleInTime;            
            DateTime outDate = request.VehicleOutDate;
            TimeSpan outTime = TimeSpan.Parse(request.VehicleOutTime);
            DateTime outDateTime = outDate.Date + outTime;

            if (outDateTime <= inDateTime)
            {
                throw new InvalidOperationException("Vehicle out time cannot be earlier than vehicle in time.");
            }

            TimeSpan totalTimeDifference = outDateTime - inDateTime;
            double totalHours = totalTimeDifference.TotalHours;
            double totalCost = 0;

            if (totalHours > MinimumHours)
                totalCost = MinimumRate + (Math.Ceiling(totalHours - MinimumHours) * AdditionalHour);
            else
            {
                totalHours = 1;
                totalCost = MinimumRate;
            }

            if (vehicleDetails != null) { 
            var query = @"
                 WITH updated_vehicle_entry  AS (
                    Update vehicle_entry 
                    SET
                        updated_by = @UpdatedBy,
                        updated_on = @UpdatedOn,
                        owner_full_name = @OwnerFullName,
                        vehicle_number = @VehicleNumber,  
                        out_date = @VehicleOutDate,
                        out_time = (CAST(@VehicleOutTime AS time)),
                        total_cost = @TotalCost,
                        total_hours = @TotalHours,
                        vehicle_status = 2
                    WHERE id = @Id
                    RETURNING *
                )
                 SELECT owner_full_name AS OwnerFullName, vehicle_number AS VehicleNumber, in_date  AS VehicleInDate,
                 out_date  AS VehicleOutDate,in_time As VehicleInTime,out_time As VehicleOutTime,                    
                 in_time AS VehicleInTime, 
                 out_time AS VehicleOutTime,
                 total_hours AS TotalHours, total_cost AS TotalCost FROM updated_vehicle_entry;
            ";


            var VehicleEntry = await _dapperWrapper.QuerySingleAsync<VehicleEntryDTO>(
                query,
                new
                {
                    id,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    OwnerFullName = request.OwnerFullName,
                    VehicleNumber = request.VehicleNumber,
                    VehicleOutDate = request.VehicleOutDate,
                    VehicleOutTime = request.VehicleOutTime,
                    TotalHours = totalHours,
                    TotalCost = totalCost
                }
            ); 
            return VehicleEntry;
            }
            else
            {
                throw new Exception("Vehicle details not found for the provided ID.");
            }
        }
    }
}

