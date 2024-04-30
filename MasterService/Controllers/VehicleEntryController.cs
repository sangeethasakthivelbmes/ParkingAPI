using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.MasterService.BusinessLayer;
using Parking.WebAPI.MasterService.BusinessLayer.Interfaces;
using Parking.WebAPI.MasterService.DTOs;
using Parking.WebAPI.MasterService.Models;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.VehicleType.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parking.WebAPI.MasterService.Controllers
{
    [Authorize]
    [Route("v1/vehicleentry")]
    [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.InternalServerError)]
    public class VehicleEntryController : Controller
    {
        private readonly IVehicleEntryBL _vehicleentryBL;
        public VehicleEntryController(IVehicleEntryBL vehicleentryBL)
        {
            _vehicleentryBL = vehicleentryBL;
        }

        // POST: v1/vehicle/inentry
        [HttpPost("Register InEntry")]
        [ProducesResponseType(typeof(ObjectId), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> RegisterVehicle([FromBody] VehicleInEntryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (request.VehicleInDate == default(DateTime))
            {
                return BadRequest("VehicleInDate is required.");
            }

            string vehicleInDateString = request.VehicleInDate.ToString("yyyy-MM-dd");

            string datePattern = @"^\d{4}-\d{2}-\d{2}$";

            if (!Regex.IsMatch(vehicleInDateString, datePattern))
            {
                return BadRequest("Invalid VehicleInDate format. Expected format is 'yyyy-MM-dd'.");
            }

            DateTime vehicleInDate;
            if (!DateTime.TryParseExact(vehicleInDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out vehicleInDate))
            {
                return BadRequest("Invalid VehicleInDate format.");
            }

            if (string.IsNullOrWhiteSpace(request.VehicleInTime))
            {
                return BadRequest("VehicleInTime is required.");
            }

            TimeSpan vehicleInTime;
            if (!TimeSpan.TryParseExact(request.VehicleInTime, "h\\:mm", CultureInfo.InvariantCulture, out vehicleInTime))
            {
                return BadRequest("Invalid VehicleInTime format. Expected format is 'h:mm'.");
            }
            var vehicle = await _vehicleentryBL.CreateVehicleInEntryAsync(request);
            return Created("", new ObjectId
            {
                Id = vehicle.Id
            });
        }

        //PUT: v1/vehicle/{outentry}
        [HttpPut("Register OutEntry")]
        [ProducesResponseType(typeof(ObjectId), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> CreateVehicleOutEntryAsync([FromBody] VehicleOutEntry request)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (request.VehicleOutEntryRequest.VehicleOutDate == default(DateTime))
            {
                return BadRequest("VehicleInDate is required.");
            }

            string vehicleInDateString = request.VehicleOutEntryRequest.VehicleOutDate.ToString("yyyy-MM-dd");

            string datePattern = @"^\d{4}-\d{2}-\d{2}$";

            if (!Regex.IsMatch(vehicleInDateString, datePattern))
            {
                return BadRequest("Invalid VehicleInDate format. Expected format is 'yyyy-MM-dd'.");
            }

            DateTime vehicleInDate;
            if (!DateTime.TryParseExact(vehicleInDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out vehicleInDate))
            {
                return BadRequest("Invalid VehicleInDate format.");
            }

            if (string.IsNullOrWhiteSpace(request.VehicleOutEntryRequest.VehicleOutTime))
            {
                return BadRequest("VehicleInTime is required.");
            }

            TimeSpan vehicleInTime;
            if (!TimeSpan.TryParseExact(request.VehicleOutEntryRequest.VehicleOutTime, "h\\:mm", CultureInfo.InvariantCulture, out vehicleInTime))
            {
                return BadRequest("Invalid VehicleInTime format. Expected format is 'h:mm'.");
            }
            var result = await _vehicleentryBL.CreateVehicleOutEntryAsync(request.vehicleId, request.VehicleOutEntryRequest);
            if (result != null)
            {
                Response.Headers.Add("description", "Vehicle out entry updated successfully.");
                return Ok(result);
            }
            else
            {
                return BadRequest(new { description = "Bad request." });
            }
        }

        public class VehicleOutEntry
        {
            public long vehicleId { get; set; }
            public VehicleOutEntryRequest VehicleOutEntryRequest { get; set; }
        }

        // GET: v1/vehicle entry}
        [HttpGet("get entry count")]
        [ProducesResponseType(typeof(IEnumerable<VehicleEntryDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetVehicleEntryCountAsync(DateTime selectedDate)
        {
            IEnumerable<VehicleStatusCountDTO> vehicleEntry = await _vehicleentryBL.GetVehicleEntryCountAsync(selectedDate);

            if (vehicleEntry != null && vehicleEntry.Any())
            {
                Response.Headers.Add("Employee-List-Status", "vehicle Entry Count list retrieved successfully.");
                return Ok(vehicleEntry);
            }
            else
                return Unauthorized(new { description = "Unauthorized request." });
        }

        // GET: v1/vehicle entry}
        [HttpGet("get entry")]
        [ProducesResponseType(typeof(IEnumerable<VehicleEntryDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetVehicleEntryAsync(DateTime selectedDate)
        {
            IEnumerable<VehicleEntryDTO> vehicleEntry = await _vehicleentryBL.GetVehicleEntryAsync(selectedDate);

            if (vehicleEntry != null && vehicleEntry.Any())
            {
                Response.Headers.Add("Employee-List-Status", "vehicle Entry list retrieved successfully.");
                return Ok(vehicleEntry);
            }
            else
                return Unauthorized(new { description = "Unauthorized request." });
        }
    }
}

