using System;
using System.Globalization;
using Dapper;
using Parking.WebAPI.CoreHelper.CryptoHelper;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.TypeHandlers;
using Parking.WebAPI.UserService.DataLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;

namespace Parking.WebAPI.UserService.DataLayer
{
    public class AuthDAL : IAuthDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        public AuthDAL(IDapperWrapper dapperWrapper)
        {
            _dapperWrapper = dapperWrapper;
        }

        public async Task<User> RegisterUserAsync(UserRegistrationRequest request)
        {
            var user = new User();
            user.Role = new Role { Id = (Int64)RoleType.User }; // 2 - User

            try
            {
                var query = @"
                        WITH inserted_user AS (
                            INSERT INTO parking_user (
                                created_by, created_on, updated_by, updated_on, is_active, 
                                role_id, name, phone, email
                            )
                            VALUES (
                                @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, @IsActive, 
                                @RoleId, @Name, @Phone, @email
                            )
                            RETURNING 
                                id, role_id AS RoleId, is_active AS IsActive, 
                                name AS Name, phone AS Phone, 
                                email AS Email
                        )
                        SELECT
                            u.id AS Id, u.IsActive, u.Name, u.Phone, u.Email,
                            json_build_object(
                                'Id', r.id, 
                                'Name', r.name, 
                                'Note', r.note,
                                'CreatedBy', r.created_by,
                                'CreatedOn', r.created_on,
                                'UpdatedBy', r.updated_by,
                                'UpdatedOn', r.updated_on,
                                'IsActive', r.is_active
                            ) as Role
                        FROM inserted_user u
                        JOIN role r ON u.RoleId = r.Id
                    ";

                SqlMapper.AddTypeHandler(typeof(Role), new JsonTypeHandler<Role>());

                var result = await _dapperWrapper.QuerySingleAsync<User>(
                    query,
                    new
                    {
                        CreatedBy = 0,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = 0,
                        UpdatedOn = DateTime.UtcNow,
                        IsActive = true,
                        RoleId = user.Role.Id,
                        request.Name,
                        request.Phone,
                        request.Email
                    }
                );
                user.Id = result.Id;
                user.Role = result.Role;
                user.Name = request.Name;
                user.Email= request.Email;
                user.Phone = request.Phone;
            }
            catch (Exception ex)
            {
                throw new Exception(" " + ex.Message.ToString(), ex);
            }
            return user;
        }

        public async Task<Transaction> SaveOTPAsync(SaveOTPRequest request)
        {
            var trans = new Transaction();
            long Id = GetUserAsyncByPhoneNo(request.Phone).GetAwaiter().GetResult();
            try
            {
                var query = @"
                        WITH inserted_transaction AS (
                            INSERT INTO transaction  (
                                created_by, created_on, updated_by, updated_on, 
                                parking_id, phone, otp
                            )
                            VALUES (
                                @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, 
                                @ParkingId, @Phone, @OTP
                            )
                            RETURNING 
                                id, parking_id AS ParkingId, phone AS Phone, otp AS OTP
                        )
                        SELECT
                            t.id AS Id, t.Phone, t.OTP,
                            json_build_object(
                                'Id', p.id, 
                                'CreatedBy', p.created_by,
                                'CreatedOn', p.created_on,
                                'UpdatedBy', p.updated_by,
                                'UpdatedOn', p.updated_on
                            ) as Transaction
                        FROM inserted_transaction t
                        JOIN parking_user p ON t.ParkingId = p.Id
                    ";

                SqlMapper.AddTypeHandler(typeof(Transaction), new JsonTypeHandler<Transaction>());

                var result = await _dapperWrapper.QuerySingleAsync<TransactionDTO>(
                    query,
                    new
                    {
                        CreatedBy = 0,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = 0,
                        UpdatedOn = DateTime.UtcNow,
                        ParkingId = Id,
                        Phone = request.Phone,
                        OTP = request.OTP
                    }
                );
                trans.Id = result.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(" " + ex.Message.ToString(), ex);
            }
            return trans;
        }

        public async Task<User> GetUserAsync(string phone)
        {
            var user = new User();

            var query = @"
                SELECT
                    u.id AS Id,email as Email, 
                    json_build_object(
                        'Id', r.id, 
                        'Name', r.name,
                        'Email','u.email',
                        'CreatedBy', r.created_by,
                        'CreatedOn', r.created_on,
                        'UpdatedBy', r.updated_by,
                        'UpdatedOn', r.updated_on
                    ) as Role
                FROM parking_user u
                JOIN role r ON u.role_id = r.Id
                WHERE u.phone = @phone
            ";
            SqlMapper.AddTypeHandler(typeof(Role), new JsonTypeHandler<Role>());
            var userRole = await _dapperWrapper.QuerySingleAsync<User>(query, new { phone });
            user.Id = userRole.Id;
            user.Role = userRole.Role;
            user.Email= userRole.Email; 

            return user;
        }
        public async Task<long> GetUserAsyncByPhoneNo(string phone)
        {
            var query = @"
                SELECT
                    id AS Id
                FROM parking_user u
                WHERE phone = @phone
            ";

            var user = await _dapperWrapper.QuerySingleAsync<User>(query, new { phone });
            return user.Id;
        }

        public async Task<bool> CheckOTPAsync(string phone,string OTP)
        {
                var query = @"
                SELECT OTP
                FROM (
                    SELECT OTP
                    FROM transaction 
                    WHERE phone = @phone
                    ORDER BY updated_on DESC
                    LIMIT 1
                ) AS last_transaction
                WHERE OTP = @otp;";

                _ = await _dapperWrapper.QuerySingleAsync<User>(query, new { phone,OTP });
                return true;
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phone, string excludePhone = "")
        {
            var query = @"
                SELECT 
                    COUNT(1) > 0 
                FROM parking_user
                WHERE
                    phone = @Phone
                    AND (@ExcludePhone = '' OR phone != @ExcludePhone)
            ";
            return await _dapperWrapper.FirstAsync<bool>(
                query,
                new
                {
                    Phone = phone,
                    ExcludePhone = excludePhone
                }
            );
        }
    }
}

