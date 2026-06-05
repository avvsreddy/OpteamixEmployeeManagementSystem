namespace EntitiesLibrary
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskPriority Priority { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public int AssignedToEmployeeId { get; set; }

        public Employee AssignedToEmployee { get; set; } = null!;

        public int CreatedByEmployeeId { get; set; }

        public Employee CreatedByEmployee { get; set; } = null!;

        public ICollection<TaskComment> Comments { get; set; }
            = new List<TaskComment>();
    }

}
