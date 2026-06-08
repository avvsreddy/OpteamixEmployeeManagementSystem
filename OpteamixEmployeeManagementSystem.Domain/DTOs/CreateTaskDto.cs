using OpteamixEmployeeManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int? EmployeeId { get; set; }

        public int ProjectId { get; set; }
    }
}