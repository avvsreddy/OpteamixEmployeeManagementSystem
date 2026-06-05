namespace EntitiesLibrary
{
    public class ProjectAssignment : BaseEntity
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public bool IsProjectManager { get; set; }

        public DateTime AssignedDate { get; set; }
    }

}
