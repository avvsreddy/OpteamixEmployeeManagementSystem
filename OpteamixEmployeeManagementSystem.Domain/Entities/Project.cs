using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; } = "Active";
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
