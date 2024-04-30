using System.Net;
using Microsoft.AspNetCore.Mvc;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DataLayer;
using Parking.WebAPI.MasterService.DataLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.UserService.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parking.WebAPI.MasterService.Controllers
{
    [Authorize]
    [Route("v1/employee")]
    [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.InternalServerError)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL _employeeBL;

        public EmployeeController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        // POST: v1/employee/Create
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ObjectId), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> Register([FromBody] EmployeeCreationRequest request)
        {
            var employee = await _employeeBL.CreateEmployeeAsync(request);            
            return Created("", new ObjectId
            {
                Id = employee.Id
            });
        }

        // GET: v1/employee}
        [HttpGet("get employees")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            IEnumerable<EmployeeDTO> employees = await _employeeBL.GetEmployeesAsync();

            if (employees != null && employees.Any())
            {
                Response.Headers.Add("Employee-List-Status", "Employees list retrieved successfully.");
                return Ok(employees);
            }
            else
                return Unauthorized(new { description = "Unauthorized request." });
        }

    }
}

