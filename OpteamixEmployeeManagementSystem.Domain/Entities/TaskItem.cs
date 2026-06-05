using OpteamixEmployeeManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        // Future FK with Employee Module
        public int EmployeeId { get; set; }

        // Future FK with Project Module
        public int ProjectId { get; set; }
    }
}