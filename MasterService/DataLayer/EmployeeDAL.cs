using Parking.WebAPI.CoreHelper.Dapper.Interfaces;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly IAuthenticatedUser _authenticatedUser;

        public EmployeeDAL(IDapperWrapper dapperWrapper, IAuthenticatedUser authenticatedUser)
        {
            _dapperWrapper = dapperWrapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var query = @"
                SELECT
                    id AS Id,
                    empname AS Name,
                    phone AS Phone,
					CASE 
                                WHEN is_active = true THEN 'Active'
                                WHEN is_active = false THEN 'InActive'
                    END As Status
                FROM employee 
            ";
            var employees = await _dapperWrapper.QueryAsync<EmployeeDTO>(query);

            return employees;
        }
        public async Task<Employee> CreateEmployeeAsync(EmployeeCreationRequest employee)
        {
            var query = @"
                        WITH inserted_employee AS (
                            INSERT INTO Employee  (
                                created_by, created_on, updated_by, updated_on, 
                                 empname, phone, is_active
                            )
                            VALUES (
                                @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, 
                                @Name, @Phone, @IsActive
                            )
                            RETURNING 
                                id,  empname AS Name, phone AS Phone,is_active AS IsActive
                        )
                        SELECT
                            id AS Id, Name, Phone,IsActive
                        FROM inserted_employee
                    ";

            var result = await _dapperWrapper.QuerySingleAsync<Employee>(
                query,
                new
                {
                    CreatedBy = 0,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = 0,
                    UpdatedOn = DateTime.UtcNow,
                    Name = employee.Name,
                    Phone = employee.Phone,
                    IsActive = true,
                }
            );
            return result;
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phone, string excludePhone = "")
        {
            var query = @"
                SELECT 
                    COUNT(1) > 0 
                FROM employee
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

