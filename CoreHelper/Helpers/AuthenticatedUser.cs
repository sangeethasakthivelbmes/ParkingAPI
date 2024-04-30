using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;

namespace Parking.WebAPI.CoreHelper.Helpers
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UserId => _httpContextAccessor.HttpContext.Request.GetHttpContextItem<long>("UserId");
        public string Name => _httpContextAccessor.HttpContext.Request.GetHttpContextItem<string>("Name");
        public string DbPointer => _httpContextAccessor.HttpContext.Request.GetHttpContextItem<string>("DbPointer");
        public string Phone => _httpContextAccessor.HttpContext.Request.GetHttpContextItem<string>("Phone");
        public long RoleId => _httpContextAccessor.HttpContext.Request.GetHttpContextItem<long>("RoleId");

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.Request.HttpContext.Items.ContainsKey("UserId");
        }
    }
}

