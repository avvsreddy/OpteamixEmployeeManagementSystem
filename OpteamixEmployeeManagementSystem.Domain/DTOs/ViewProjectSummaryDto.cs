using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class ViewProjectSummaryDto
    {
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int InactiveProjects { get; set; }
        public int CompletedProjects { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
