using OpteamixEmployeeManagementSystem.Domain.Enums;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class TaskDto
    {
        public int TaskItemId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int? EmployeeId { get; set; }

        public int ProjectId { get; set; }
    }
}