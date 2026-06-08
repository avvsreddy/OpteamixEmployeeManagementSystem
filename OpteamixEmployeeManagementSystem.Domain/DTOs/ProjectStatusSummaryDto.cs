using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class ProjectStatusSummaryDto
    {
        public int ActiveProjects { get; set; }
        public int InactiveProjects { get; set; }
        public int CompletedProjects { get; set; }
    }
}
