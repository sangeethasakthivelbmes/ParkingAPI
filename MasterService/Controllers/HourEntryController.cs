using System.Net;
using Microsoft.AspNetCore.Mvc;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.MasterService.BusinessLayer;
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
    [Route("v1/hoursetting")]
    [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.InternalServerError)]
    public class HourEntryController : Controller
    {
        private readonly IHourEntryBL _hoursettingBL;

        public HourEntryController(IHourEntryBL hoursettingBL)
        {
            _hoursettingBL = hoursettingBL;
        }

        // POST: v1/hoursetting/Create
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ObjectId), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> Register([FromBody] HourEntryRequest request)
        {
            var hoursetting = await _hoursettingBL.CreateHourSettingAsync(request);
            return Created("", new ObjectId
            {
                Id = hoursetting.Id
            });
        }

    }
}

