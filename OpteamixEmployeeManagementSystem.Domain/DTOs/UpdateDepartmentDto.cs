namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class UpdateDepartmentDto
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}