using System.Net;
using Microsoft.AspNetCore.Mvc;
using Parking.WebAPI.CoreHelper.Attributes;
using Parking.WebAPI.CoreHelper.Models;
using Parking.WebAPI.UserService.BusinessLayer;
using Parking.WebAPI.UserService.BusinessLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;

namespace Parking.WebAPI.UserService.Controllers
{
    [Authorize]
    [Route("v1/users")]
    [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.InternalServerError)]
    public class UserController : Controller
    {
        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        // GET: v1/users/profile
        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomError), (int)HttpStatusCode.Unauthorized)]
        public async Task<UserDTO> GetAuthenticatedUserAsync()
        {
            return await _userBL.GetAuthenticatedUserAsync();
        }

        //PUT: v1/users/update
        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(List<CustomError>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateUser([FromForm] UpdateUserRequest request)
        {
            if (request.UserId != null)
            {
                try
                {
                    var userFolderPath = Path.Combine(@"D:\Parking.WebAPI\profile-image\", request.UserId.ToString());                    

                    if (request.File != null)
                    {
                        if (Directory.Exists(userFolderPath))
                        {
                            foreach (var Path in Directory.GetFiles(userFolderPath))
                            {
                                System.IO.File.Delete(Path);
                            }
                        }
                        else
                            Directory.CreateDirectory(userFolderPath);

                        var filePath = Path.Combine(userFolderPath, request.File.FileName);   


                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await request.File.CopyToAsync(stream);
                        }
                    }

                    bool success = await _userBL.UpdateUserAsync(request.UserId, request.UserUpdationRequest);
                    if (success)
                    {
                        Response.Headers.Add("description", "User information updated successfully.");
                        return NoContent();
                    }
                    else
                    {
                        return Ok(new { description = "User information updated successfully." });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { description = "An error occurred while processing the request.", error = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { description = "UserId is required." });
            }
        }
    }    
    public class UpdateUserRequest
    {
        public long UserId { get; set; }
        public UserUpdationRequest UserUpdationRequest { get; set; }
        public IFormFile File { get; set; }
    }
}



