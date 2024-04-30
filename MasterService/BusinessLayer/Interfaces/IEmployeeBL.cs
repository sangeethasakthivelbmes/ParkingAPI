using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

namespace Parking.WebAPI.MasterService.BusinessLayer.Interfaces
{
    public interface IEmployeeBL
    {
        public Task<Employee> CreateEmployeeAsync(EmployeeCreationRequest request);
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
    }
}

