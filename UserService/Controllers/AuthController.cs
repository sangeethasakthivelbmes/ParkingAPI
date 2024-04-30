using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.Model;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.UserService.BusinessLayer;
using Parking.WebAPI.UserService.BusinessLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingAPI.UserService.Controllers
{
    [Route("v1/auth")]
    [ProducesResponseType(typeof(CustomError), (int) HttpStatusCode.InternalServerError)]
    public class AuthController : Controller
    {
        private readonly IAuthBL _authBL;
        private readonly IJWTHelper _jwtHelper;

        public AuthController(IAuthBL authBL, IJWTHelper jwtHelper)
        {
            _authBL = authBL;
            _jwtHelper = jwtHelper;
        }

        // POST v1/auth/register
        [HttpPost("Create new Account")]
        [ProducesResponseType(typeof(ObjectId), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<CustomError>), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ObjectId>> Register([FromBody]UserRegistrationRequest request)
        {
            var user = await _authBL.RegisterUserAsync(request);
            SignIn(user);
            return Created("", new ObjectId{
                Id = user.Id
            });
        }

        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomError>), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Login([FromBody]UserLoginRequest request)
        {
            var user = await _authBL.AuthenticateUserAsync(request);
            if (user != null)
            {
                Response.Headers.Add("Login-Status", "OTP sent successfully.");
                return Ok();
            }
            else
                return Unauthorized(new { description = "Unauthorized request." });
        }

        [HttpPost("Confirm OTP")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ConfirmLogin([FromBody] UserConfirmLoginRequest request)
        {
            bool success  = await _authBL.ConfirmLoginAsync(request);
            if (success)
            {
                Response.Headers.Add("description", "Login success.");
                return NoContent();
            }
            else
                return BadRequest(new { description = "Bad request." });
        }

        #region Private methods
        private void SetCookie(string token)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.MaxValue,
                HttpOnly = true,
                Path = "/; SameSite=None",
                Secure = true,
                Domain = Request.Host.Host
            };
            Response.Cookies.Append("_at", token, option);
            Response.Cookies.Append("userLanguage", CultureInfo.CurrentCulture.Name, new CookieOptions { Expires = DateTime.MaxValue });
        }
        private void SignIn(User user)
        {
            var token = _jwtHelper.GenerateToken(new AuthUser{
                UserId = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Email= user.Email,
                RoleId = user.Role.Id
            });
            SetCookie(token);
        }
        private void RemoveAuthCookie()
        {
            var option = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Path = "/; SameSite=None",
                Secure = true,
                Domain = Request.Host.Host
            };
            Response.Cookies.Append("_at", "", option);
            Response.Cookies.Append("userLanguage", CultureInfo.CurrentCulture.Name, new CookieOptions { Expires = DateTime.MaxValue });
        }
        #endregion
        
    }
}

