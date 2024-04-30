using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.DataLayer.Interfaces
{
    public interface IEmployeeDAL
    {
        public Task<Employee> CreateEmployeeAsync(EmployeeCreationRequest request); 
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<bool> IsPhoneNumberExistsAsync(string phone, string excludePhone = "");
    }
}

