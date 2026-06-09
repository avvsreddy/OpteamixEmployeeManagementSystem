using OpteamixEmployeeManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class TaskItem
    {
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }
            = TaskItemStatus.New;

        public TaskPriority Priority { get; set; }

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}