namespace EntitiesLibrary
{
    public class Employee : BaseEntity
    {
        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfJoining { get; set; }

        public string Designation { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        public User? User { get; set; }

        public ICollection<ProjectAssignment> ProjectAssignments { get; set; }
            = new List<ProjectAssignment>();

        public ICollection<TaskItem> AssignedTasks { get; set; }
            = new List<TaskItem>();

        public ICollection<TaskComment> Comments { get; set; }
            = new List<TaskComment>();
    }

}
