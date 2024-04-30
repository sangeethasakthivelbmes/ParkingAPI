namespace Parking.WebAPI.MasterService.DTOs
{
    public class EmployeeCreationRequest
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}

