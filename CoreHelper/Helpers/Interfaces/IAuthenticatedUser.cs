namespace Parking.WebAPI.CoreHelper.Helpers.Interfaces
{
    public interface IAuthenticatedUser
    {
        public Int64 UserId { get;}
        public string Name { get; }
        public string DbPointer { get; }
        public string Phone { get; }
        public Int64 RoleId { get; }
        public bool IsAuthenticated();
    }
}

