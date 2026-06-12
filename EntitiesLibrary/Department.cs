using EntitiesLibrary;
using System.Collections.Generic;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
