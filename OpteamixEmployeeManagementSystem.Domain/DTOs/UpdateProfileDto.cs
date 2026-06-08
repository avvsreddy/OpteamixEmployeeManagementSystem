namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class UpdateProfileDto
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? EmployeeCode { get; set; }
    }
}