using Parking.WebAPI.EmployeeType.Validators;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.UserService.Validators;
using System.Net;

using Parking.WebAPI.CoreHelper.Extensions;
using static System.Net.WebRequestMethods;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;
using Parking.WebAPI.UserService.DataLayer.Interfaces;
using Parking.WebAPI.Resources.UserService;
using FluentValidation.Results;

namespace Parking.WebAPI.MasterService.BusinessLayer
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IAuthDAL _authDAL;

        public EmployeeBL(IAuthDAL authDAL,IEmployeeDAL employeeDAL)
        {
            _authDAL = authDAL;
            _employeeDAL = employeeDAL;
        }

        public async Task<Employee> CreateEmployeeAsync(EmployeeCreationRequest request)
        {
            request.Validate<EmployeeCreationRequest, EmployeeCreationRequestValidator>();
            var isPhonenumberExists = await _employeeDAL.IsPhoneNumberExistsAsync(request.Phone);
            if (isPhonenumberExists)
            {
                throw new List<ValidationFailure>{
                    new ValidationFailure {
                        ErrorCode = nameof(Resources.UserService.User.US_User_001),
                        ErrorMessage = Resources.UserService.User.US_User_001
                    }
                }.CustomException(HttpStatusCode.BadRequest);
            }
            return await _employeeDAL.CreateEmployeeAsync(request);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync() => await _employeeDAL.GetEmployeesAsync();

    }
}

