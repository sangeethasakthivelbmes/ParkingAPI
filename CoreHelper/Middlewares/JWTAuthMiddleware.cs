using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Middlewares
{
    public class JWTAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public JWTAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJWTHelper jwtHelper)
        {
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            if (context.Request.Cookies.TryGetValue("_at", out var token))
            {
                AuthUser user = jwtHelper.ValidateToken(token);
                context.Response.HttpContext.Items.Add("UserId", user.UserId);
                context.Response.HttpContext.Items.Add("RoleId", user.RoleId);
                context.Response.HttpContext.Items.Add("Phone", user.Phone);
                context.Response.HttpContext.Items.Add("Name", user.Name);
                context.Response.HttpContext.Items.Add("Email", user.Email);
            }

            await _next(context);
        }
    }
}

