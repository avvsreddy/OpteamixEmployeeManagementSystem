namespace EntitiesLibrary
{
    public class Project : BaseEntity
    {
        public string ProjectCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public ProjectStatus Status { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; } = null!;

        public ICollection<ProjectAssignment> ProjectAssignments { get; set; }
            = new List<ProjectAssignment>();

        public ICollection<TaskItem> Tasks { get; set; }
            = new List<TaskItem>();
    }

}
