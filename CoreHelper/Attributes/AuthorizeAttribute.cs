using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticatedUser = context.HttpContext.RequestServices.GetService<IAuthenticatedUser>();
            
            if (!authenticatedUser.IsAuthenticated())
            {
                var error = new CustomError
                {
                    ErrorCode = "WIS_000",
                    Message = "Unauthorized",
                    LanguageCode = "en-GB"
                };
                context.Result = new JsonResult(error) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}

