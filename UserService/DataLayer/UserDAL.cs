using Dapper;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.TypeHandlers;
using Parking.WebAPI.UserService.DataLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;
using System.Globalization;

namespace Parking.WebAPI.UserService.DataLayer
{
    public class UserDAL : IUserDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly ILogger<UserDAL> _logger;
        private readonly IAuthenticatedUser _authenticatedUser;

        public UserDAL(ILogger<UserDAL> logger, IDapperWrapper dapperWrapper, IAuthenticatedUser authenticatedUser)
        {
            _logger = logger;
            _dapperWrapper = dapperWrapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<UserDTO> GetAuthenticatedUserAsync()
        {
            return await GetUserAsync(_authenticatedUser.UserId);
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            var query = @"
                SELECT
                    u.id AS Id,
                    u.name AS Name,
                    u.phone AS Phone,
                    u.email AS Email,
                    u.addresses AS Addresses,
                    u.stand_name AS StandName,
                    json_build_object(
                        'Id', r.id, 
                        'Name', r.name
                    ) as Role,
                    u.note AS Note
                FROM parking_user u
                JOIN role r ON u.role_id = r.Id
                WHERE u.id = @id
            ";
            SqlMapper.AddTypeHandler(typeof(RoleDTO), new JsonTypeHandler<RoleDTO>());
            var user = await _dapperWrapper.QuerySingleAsync<UserDTO>(query, new { id });
            return user;
        }

        public async Task<UserDTO> GetUserAsyncByPhoneNo(long phone)
        {
            var query = @"
                SELECT
                    u.id AS Id,
                    u.name AS Name,
                    u.phone AS Phone,
                    u.email AS Email
                FROM parking_user u
                WHERE u.phone = @phone
            ";

            var user = await _dapperWrapper.QuerySingleAsync<UserDTO>(query, new { phone });
            return user;
        }

        public async Task<bool> UpdateUserAsync(long id, UserUpdationRequest request)
        {
            var existingUser = await GetUserByPhoneOrEmailAsync(request.Phone, request.Email);

            if (existingUser != null && existingUser.Id != id)
            {
                return false;
            }

            int affectedRows = 0;
            var query = @"
                WITH updated_user AS (
                UPDATE parking_user
                SET
                    updated_by = @UpdatedBy,
                    updated_on = @UpdatedOn,
                    name = CASE WHEN @Name <> '' AND @Name <> 'string' THEN @Name ELSE name END,
                    phone = CASE WHEN @Phone <> '' AND @Phone <> 'string' THEN @Phone ELSE phone END,
                    email = CASE WHEN @Email <> '' AND @Email <> 'string' THEN @Email ELSE email END,
                    stand_name = CASE WHEN @StandName <> '' AND @StandName <> 'string' THEN @StandName ELSE stand_name END,
                    addresses = CASE WHEN @Addresses <> '' AND @Addresses <> 'string' THEN @Addresses ELSE addresses END
                WHERE id = @Id
                RETURNING *
            )
            INSERT INTO log_table (operation_type, table_name, user_id, row_data)
            SELECT 
                'Update' AS operation_type,
                'parking_user' AS table_name,
                @Id AS user_id,
                jsonb_object_agg(
                    key, 
                    CASE WHEN updated_value <> original_value THEN updated_value ELSE NULL END
                ) AS changed_columns
            FROM 
                updated_user
            CROSS JOIN LATERAL (
                SELECT 
                    key, 
                    t.value AS updated_value,
                    (SELECT value FROM jsonb_each_text(to_jsonb(updated_user)) WHERE key = t.key) AS original_value
                FROM 
                    jsonb_each_text(to_jsonb(updated_user)) AS t(key, value)
            ) AS t(key, updated_value, original_value)
            GROUP BY
                user_id
            ";

            var updatedRows = await _dapperWrapper.QueryAsync<dynamic>(
                query,
                new
                {
                    id,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    Name = request.Name,
                    Phone = request.Phone,
                    Email = request.Email,
                    StandName = request.StandName,
                    Addresses = request.Addresses
                }
            );
            affectedRows = updatedRows.Count();
            return affectedRows == 1;
        }

        private async Task<User> GetUserByPhoneOrEmailAsync(string phone, string email)
        {
            var query = @"
            SELECT * FROM parking_user
            WHERE Phone = @Phone OR Email = @Email;
            ";
            var result = await _dapperWrapper.QueryAsync<User>(query, new { Phone = phone, Email = email });

            return result.FirstOrDefault();
        }
    }
}

