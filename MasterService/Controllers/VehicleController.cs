using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.MasterService.BusinessLayer;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parking.WebAPI.MasterService.Controllers
{
    [Authorize]
    [Route("v1/vehicle")]
    [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.InternalServerError)]
    public class VehicleController : Controller
    {
        private readonly IVehicleBL _vehicleBL;

        public VehicleController(IVehicleBL vehicleBL)
        {
            _vehicleBL = vehicleBL;
        }

        //PUT: v1/vehicle/{vehicleId}
        [HttpPut("update")]
        [ProducesResponseType(typeof(ObjectId), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> UpdateVehicleType([FromBody] List<VehicleTypeDTO> requests)
        {
            bool success = await _vehicleBL.UpdateVehicleTypesAsync(requests);
            if (success)
            {
                Response.Headers.Add("description", "vehicle information updated successfully.");
                return NoContent();
            }
            else
                return BadRequest(new { description = "Bad request." });
        }

        // GET: v1/vehicletypes}
        [HttpGet("get vehicle types")]
        [ProducesResponseType(typeof(IEnumerable<VehicleTypeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetVehicleTypesAsync()
        {
            IEnumerable<VehicleTypeDTO> vehicles = await _vehicleBL.GetVehicleTypesAsync();

            if (vehicles != null && vehicles.Any())
            {
                Response.Headers.Add("Vehicle-Types-Status", "Vehicle Types retrieved successfully.");
                return Ok(vehicles);
            }
            else
                return Unauthorized(new { description = "Unauthorized request." });
        }

    }
}

